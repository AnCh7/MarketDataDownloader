#region Usings

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using MDD.Library.Abstraction.Client;
using MDD.Library.Helpers;
using MDD.Library.Logging;

#endregion

namespace MDD.DataFeed.Fidelity.DataProvider
{
	public class FidelityClient : IDataFeedClient
	{
		private readonly IMyLogger _logger;

		public FidelityClient(IMyLogger logger)
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
			ConnectionEstablished.Invoke(true, "Connected");
		}

		public void Disconnect()
		{
			ConnectionEstablished.Invoke(false, "Disconnected");
		}

		public async Task StartDownloadingAsync(string request)
		{
			try
			{
				var webRequest = (HttpWebRequest)WebRequest.Create(request);
				webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
				var asyncResult = webRequest.BeginGetResponse(null, null);

				WaitHandle.WaitAny(new[] {asyncResult.AsyncWaitHandle, CancelationHelper.TokenSource.Token.WaitHandle});

				if (CancelationHelper.TokenSource.Token.IsCancellationRequested)
				{
					webRequest.Abort();
					return;
				}

				ReadFromResponseStream(webRequest.EndGetResponse(asyncResult));
			}
			catch (TaskCanceledException)
			{}
			catch (Exception ex)
			{
				_logger.Error(string.Format("[StartDownloadingAsync] {0}", ex.Message));
			}
			finally
			{
				DataflowHelper.ResponsesQueue.Complete();
			}
		}

		private void ReadFromResponseStream(WebResponse response)
		{
			try
			{
				using (var responseStream = response.GetResponseStream())
				{
					if (responseStream != null)
					{
						using (var reader = new StreamReader(responseStream))
						{
							var str = reader.ReadToEnd();
							DataflowHelper.ResponsesQueue.Post(str);
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Error(string.Format("[ReadFromResponseStream] {0}", ex.Message));
			}
			finally
			{
				response.Close();
				response.Dispose();
			}
		}
	}
}
