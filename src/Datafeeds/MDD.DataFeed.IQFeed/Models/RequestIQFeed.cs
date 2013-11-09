#region Usings

using System;
using System.Collections.Generic;

using MDD.Library.Abstraction;
using MDD.Library.Configuration;

#endregion

namespace MDD.DataFeed.IQFeed.Models
{
	public sealed class RequestIQFeed : RequestBase
	{
		public RequestIQFeed(string cbTimeframeSelectedItem, bool rbDaysChecked, bool rbIntervalChecked,
							 string tbAmountOfDaysText, DateTime dtpStartDateTimeValue, DateTime dtpEndDateTimeValue,
							 bool rbMainSessionChecked, bool rbAlldataChecked, string cbTimeframeIntradaySelectedItem,
							 bool rbCustomIntChecked, DateTime dtpCustomIntBeginValue, DateTime dtpCustomIntEndValue, List<string> tickers)
		{
			RequestID = string.Empty;
			Symbols = tickers;
			CurrentSymbol = string.Empty;
			TimeFrameName = cbTimeframeSelectedItem;

			if (rbDaysChecked)
			{
				TimeFrameType = "Days";
			}
			if (rbIntervalChecked)
			{
				TimeFrameType = "Interval";
			}

			AmountOfDays = tbAmountOfDaysText;

			int interval;
			Int32.TryParse(cbTimeframeIntradaySelectedItem, out interval);
			Interval = interval;

			BeginDateTime = dtpStartDateTimeValue;
			EndDateTime = dtpEndDateTimeValue;

			if (rbCustomIntChecked)
			{
				BeginFilterTime = dtpCustomIntBeginValue.ToString("HHmmss");
				EndFilterTime = dtpCustomIntEndValue.ToString("HHmmss");
			}

			if (rbMainSessionChecked)
			{
				BeginFilterTime = Cfg.NYSEOpenTime;
				EndFilterTime = Cfg.NYSECloseTime;
			}

			if (rbAlldataChecked)
			{
				BeginFilterTime = Cfg.DayOpen;
				EndFilterTime = Cfg.DayClose;
			}

			Reserved = string.Empty;
			MaxDatapoints = "0";
			DatapointsPerSend = "2500";
			DataDirection = "1";
			IntervalType = "s";
		}

		/// <summary>
		///     Required - The maximum number of datapoints to be retrieved.
		/// </summary>
		public string MaxDatapoints { get; private set; }

		/// <summary>
		///     Optional - '0' (default) for "newest to oldest" or '1' for "oldest to newest".
		/// </summary>
		public string DataDirection { get; private set; }

		/// <summary>
		///     Optional - Specifies the number of datapoints that IQConnect.exe will queue before attempting to send across the
		///     socket to your app.
		/// </summary>
		public string DatapointsPerSend { get; private set; }

		/// <summary>
		///     Optional - 's' (default) for time intervals in seconds, 'v' for volume intervals, 't' for tick intervals
		/// </summary>
		public string IntervalType { get; private set; }

		/// <summary> This parameter should always be empty. </summary>
		public string Reserved { get; private set; }

		public string TimeFrame
		{
			get { return base.TimeFrameName + " " + base.TimeFrameType; }
		}

		public string GetBeginDateTime
		{
			get { return base.BeginDateTime.ToString("yyyyMMdd HHmmss"); }
		}

		public string GetEndDateTime
		{
			get { return base.EndDateTime.ToString("yyyyMMdd HHmmss"); }
		}

		public int GetInterval
		{
			get { return base.Interval * 60; }
		}
	}
}
