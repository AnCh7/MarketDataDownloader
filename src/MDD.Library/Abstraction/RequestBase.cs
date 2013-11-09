#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace MDD.Library.Abstraction
{
	public abstract class RequestBase
	{
		public virtual string RequestID { get; set; }
		public virtual List<string> Symbols { get; set; }
		public virtual string CurrentSymbol { get; set; }
		public virtual DateTime BeginDateTime { get; set; }
		public virtual DateTime EndDateTime { get; set; }
		public virtual string BeginFilterTime { get; set; }
		public virtual string EndFilterTime { get; set; }
		public virtual string TimeFrameName { get; set; }
		public virtual string TimeFrameType { get; set; }
		public virtual int Interval { get; set; }
		public virtual string AmountOfDays { get; set; }
	}
}
