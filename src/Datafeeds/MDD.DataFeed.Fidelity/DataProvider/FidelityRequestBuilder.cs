#region Usings

using System;
using System.Collections.Generic;
using System.Text;

using MDD.DataFeed.Fidelity.Configuration;
using MDD.DataFeed.Fidelity.Models;
using MDD.Library.Abstraction;
using MDD.Library.Helpers;
using MDD.Library.Logging;

#endregion

namespace MDD.DataFeed.Fidelity.DataProvider
{
	public class FidelityRequestBuilder : IRequestBuilder
	{
		private readonly IMyLogger _logger;

		public FidelityRequestBuilder(IMyLogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}

			_logger = logger;
		}

		public List<string> CreateRequests(RequestBase request)
		{
			var requests = new List<string>();

			if (request == null)
			{
				_logger.Error("[CreateRequests] Request ArgumentNullException");
				return requests;
			}

			var r = (RequestFidelity)request;

			try
			{
				var dateTimeLimit = (r.TimeFrameName == "Intraday" ? DateTimeHelper.Date2004 : DateTimeHelper.Date1971);
				var chunkSize = (r.TimeFrameName == "Intraday" ? FidelityCfg.FidelityDaysChunk : 0);

				var dates = GetDates(r, r.TimeFrameName, dateTimeLimit, chunkSize);
				foreach (var date in dates)
				{
					var b = new StringBuilder();
					requests.Add(b.ToString());
				}
			}
			catch (Exception ex)
			{
				_logger.Error(string.Format("[CreateRequest] {0}", ex.Message));
			}

			return requests;
		}

		private Dictionary<string, string> GetDates(RequestFidelity r, string timeFrameName, DateTime dateTimeLimit,
													int chunkSize)
		{
			var dates = new Dictionary<string, string>();

			DateTime beginDateTime;
			DateTime endDateTime;

			if (r.TimeFrameType == "Days")
			{
				beginDateTime = DateTimeHelper.NowEST.AddDays(-Convert.ToDouble(r.AmountOfDays));

				if (beginDateTime < dateTimeLimit)
				{
					beginDateTime = dateTimeLimit;
				}

				endDateTime = DateTimeHelper.NowEST;

				DateTimeHelper.SplitDatesByChunkForFidelity(timeFrameName, chunkSize, beginDateTime, endDateTime, dates);
			}

			if (r.TimeFrameType == "Interval")
			{
				if (r.BeginDateTime.Date < dateTimeLimit)
				{
					r.BeginDateTime = dateTimeLimit;
				}

				beginDateTime = r.BeginDateTime;
				endDateTime = r.EndDateTime;

				DateTimeHelper.SplitDatesByChunkForFidelity(timeFrameName, chunkSize, beginDateTime, endDateTime, dates);
			}

			return dates;
		}

		private int GetGranularuty(string timeFrameName, int interval)
		{
			var granularity = 0;
			return granularity;
		}
	}
}
