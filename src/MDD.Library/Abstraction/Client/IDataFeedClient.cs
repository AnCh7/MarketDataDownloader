#region Usings

using System;
using System.Threading.Tasks;

#endregion

namespace MDD.Library.Abstraction.Client
{
	public interface IDataFeedClient
	{
		Action<bool, string> ConnectionEstablished { get; set; }

		void Connect();

		void Disconnect();

		Task StartDownloadingAsync(string request);
	}
}
