#region Usings

using System;
using System.Collections.Generic;

using MDD.Library.Abstraction;

#endregion

namespace MDD.DataFeed.Fidelity.Models
{
	public sealed class RequestFidelity : RequestBase
	{
		public RequestFidelity(string cbTimeframeSelectedItem, bool rbDaysChecked, bool rbIntervalChecked,
							   string tbAmountOfDaysText, DateTime dtpStartDateTimeValue, DateTime dtpEndDateTimeValue,
							   bool rbMainSessionChecked, bool rbAlldataChecked, string cbTimeframeIntradaySelectedItem, List<string> tickers)
		{
			TimeFrameName = cbTimeframeSelectedItem;

			int interval;
			Int32.TryParse(cbTimeframeIntradaySelectedItem, out interval);
			Interval = interval;

			switch (cbTimeframeSelectedItem)
			{
				case "Daily":
					IsIntraday = false;
					break;

				case "Weekly":
					IsIntraday = false;
					break;

				case "Monthly":
					IsIntraday = false;
					break;

				case "Intraday":
					IsIntraday = true;
					break;
			}

			RequestID = string.Empty;
			Symbols = tickers;
			CurrentSymbol = string.Empty;

			if (rbDaysChecked)
			{
				TimeFrameType = "Days";
			}
			if (rbIntervalChecked)
			{
				TimeFrameType = "Interval";
			}

			AmountOfDays = tbAmountOfDaysText;

			BeginDateTime = dtpStartDateTimeValue;
			EndDateTime = dtpEndDateTimeValue;

			BeginFilterTime = string.Empty;
			EndFilterTime = string.Empty;

			if (rbMainSessionChecked)
			{
				ExtendedMarket = false;
			}
			if (rbAlldataChecked)
			{
				ExtendedMarket = true;
			}
		}

		public bool ExtendedMarket { get; set; }

		public bool IsIntraday { get; set; }
	}
}
