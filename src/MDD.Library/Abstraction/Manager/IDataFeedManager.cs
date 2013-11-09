#region Usings

using System;
using System.Threading.Tasks;

using MDD.Library.Models;

#endregion

namespace MDD.Library.Abstraction.Manager
{
	public interface IDataFeedManager
	{
		Action<bool, string> ConnectionEstablished { get; set; }

		void StartService();

		void StopService();

		Task DownloadMarketData(RequestBase requestParameters, Parameters programParameters);
	}
}
