#region Usings

using System;

#endregion

namespace MDD.Library.Abstraction
{
	public abstract class MarketDataBase
	{
		public virtual string Id { get; set; }
		public virtual DateTime TimeStamp { get; set; }
		public virtual string High { get; set; }
		public virtual string Low { get; set; }
		public virtual string Open { get; set; }
		public virtual string Close { get; set; }
		public virtual string Volume { get; set; }
		public virtual string OpenInterest { get; set; }
		public virtual string Last { get; set; }
		public virtual string LastSize { get; set; }
		public virtual string TotalVolume { get; set; }
		public virtual string PeriodVolume { get; set; }
		public virtual string Bid { get; set; }
		public virtual string Ask { get; set; }
		public virtual string TickId { get; set; }
		public virtual string BidSize { get; set; }
		public virtual string AskSize { get; set; }
		public virtual string TradeType { get; set; }
	}
}
