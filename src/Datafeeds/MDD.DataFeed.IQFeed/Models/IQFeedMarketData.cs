#region Usings

using MDD.Library.Abstraction;

#endregion

namespace MDD.DataFeed.IQFeed.Models
{
	public class IQFeedMarketData : MarketDataBase
	{
		public string RequestType { get; set; }
		public string BasisForLast { get; set; }
		public string TradeMarketCenter { get; set; }
		public string TradeConditions { get; set; }
	}
}
