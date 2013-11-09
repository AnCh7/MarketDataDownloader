#region Usings

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using MDD.DataFeed.IQFeed.Configuration;
using MDD.Library.Abstraction.Client;
using MDD.Library.Helpers;
using MDD.Library.Logging;

#endregion

namespace MDD.DataFeed.IQFeed.DataProvider
{
	public class IQFeedSocketClient : IDataFeedClient
	{
		private readonly IMyLogger _logger;
		private Socket _socket;

		public IQFeedSocketClient(IMyLogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}

			_logger = logger;
		}

		public Action<bool, string> ConnectionEstablished { get; set; }

		public void Connect()
		{
			try
			{
				_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				_socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

				var e = new SocketAsyncEventArgs();
				e.RemoteEndPoint = new IPEndPoint(IPAddress.Loopback, IQFeedCfg.IQFeedLookup);
				e.Completed += OnSocketAsyncEventCompleted;

				if (!_socket.ConnectAsync(e))
				{
					OnSocketAsyncEventCompleted(this, e);
				}
			}
			catch (Exception ex)
			{
				_logger.Error(string.Format("[Connect] {0}", ex.Message));
			}
		}

		public void Disconnect()
		{
			try
			{
				if (_socket.Connected)
				{
					var e = new SocketAsyncEventArgs();
					e.Completed += OnSocketAsyncEventCompleted;

					_socket.DisconnectAsync(e);
				}
			}
			catch (Exception ex)
			{
				_logger.Error(string.Format("[Disconnect] {0}", ex.Message));
			}
		}

		public async Task StartDownloadingAsync(string request)
		{
			Send(request);
		}

		private void Send(string request)
		{
			try
			{
				if (_socket != null && (_socket.Connected && request.Length > 0))
				{
					var bytes = Encoding.UTF8.GetBytes(request);

					var e = new SocketAsyncEventArgs();
					e.SetBuffer(bytes, 0, bytes.Length);
					e.Completed += OnSocketAsyncEventCompleted;
					e.UserToken = _socket;

					if (!_socket.SendAsync(e))
					{
						OnSocketAsyncEventCompleted(this, e);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Error(string.Format("[Send] {0}", ex.Message));
			}
		}

		private void OnSocketAsyncEventCompleted(object sender, SocketAsyncEventArgs e)
		{
			try
			{
				switch (e.LastOperation)
				{
					case SocketAsyncOperation.Connect:
						UpdateConnectionStatus(e);
						Send(IQFeedCfg.ProtocolRequest);
						break;

					case SocketAsyncOperation.Disconnect:
						_socket.Shutdown(SocketShutdown.Both);
						_socket.Close();
						ConnectionEstablished.Invoke(false, "Disconnected");
						break;

					case SocketAsyncOperation.Receive:
						break;

					case SocketAsyncOperation.Send:
						Download();
						break;

					default:
						_logger.Error(string.Format("[Not supported SocketAsyncOperation] {0}", e.LastOperation));
						break;
				}
			}
			catch (Exception ex)
			{
				_logger.Error(string.Format("[OnSocketAsyncEventCompleted] {0}", ex.Message));
			}
		}

		private void UpdateConnectionStatus(SocketAsyncEventArgs e)
		{
			if (e.ConnectSocket != null && e.ConnectSocket.Connected && e.SocketError == SocketError.Success)
			{
				ConnectionEstablished.Invoke(true, string.Format("Socket connected to {0}", _socket.RemoteEndPoint));
			}
			else
			{
				ConnectionEstablished.Invoke(false, "Can't connect to the datafeed. Check IQLink connection");
			}
		}

		private void Download()
		{
			try
			{
				using (var networkStream = new NetworkStream(_socket))
				{
					while (true)
					{
						if (CancelationHelper.TokenSource.Token.IsCancellationRequested)
						{
							return;
						}

						var bytes = new byte[65536];
						networkStream.Read(bytes, 0, bytes.Length);

						var str = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
						DataflowHelper.ResponsesQueue.Post(str);

						if (str.Contains(IQFeedCfg.EndMessage) || str.Contains(IQFeedCfg.ProtocolResponse) ||
							str.Contains(IQFeedCfg.NoData) || str.Contains(IQFeedCfg.InvalidSymbol))
						{
							break;
						}
					}
				}
			}
			catch (TaskCanceledException)
			{}
			catch (IOException)
			{}
			catch (Exception ex)
			{
				_logger.Error(string.Format("[Downloading] {0}", ex.Message));
			}
			finally
			{
				DataflowHelper.ResponsesQueue.Complete();
			}
		}
	}
}
