#region Usings

using System;
using System.Collections.Generic;
using System.Text;

using MDD.DataFeed.IQFeed.Configuration;
using MDD.DataFeed.IQFeed.Models;
using MDD.Library.Abstraction;
using MDD.Library.Logging;

#endregion

namespace MDD.DataFeed.IQFeed.DataProvider
{
	public class IQFeedRequestBuilder : IRequestBuilder
	{
		private readonly IMyLogger _logger;

		public IQFeedRequestBuilder(IMyLogger logger)
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

			var r = (RequestIQFeed)request;
			var query = new StringBuilder();

			switch (r.TimeFrame)
			{
				case "Tick Days":
					//HTD,[Symbol],[Days],[MaxDatapoints],[BeginFilterTime],[EndFilterTime],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
					//Retrieves ticks for the previous [Days] days for the specified [Symbol].
					query.Append("HTD");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.CurrentSymbol);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.AmountOfDays);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.MaxDatapoints);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.BeginFilterTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.EndFilterTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DataDirection);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.RequestID = "HTD");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DatapointsPerSend);
					query.Append(IQFeedCfg.Terminater);
					break;

				case "Tick Interval":
					//HTT,[Symbol],[BeginDate BeginTime],[EndDate EndTime],[MaxDatapoints],[BeginFilterTime],[EndFilterTime],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
					//Retrieves tick data between [BeginDate BeginTime] and [EndDate EndTime] for the specified [Symbol].
					query.Append("HTT");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.CurrentSymbol);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.GetBeginDateTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.GetEndDateTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.MaxDatapoints);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.BeginFilterTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.EndFilterTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DataDirection);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.RequestID = "HTT");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DatapointsPerSend);
					query.Append(IQFeedCfg.Terminater);
					break;

				case "Intraday Days":
					//HID,[Symbol],[Interval],[Days],[MaxDatapoints],[BeginFilterTime],[EndFilterTime],[DataDirection],[RequestID],[DatapointsPerSend],[IntervalType]<CR><LF> 
					//Retrieves [Days] days of interval data for the specified [Symbol].
					query.Append("HID");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.CurrentSymbol);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.GetInterval);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.AmountOfDays);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.MaxDatapoints);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.BeginFilterTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.EndFilterTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DataDirection);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.RequestID = "HID");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DatapointsPerSend);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.IntervalType);
					query.Append(IQFeedCfg.Terminater);
					break;

				case "Intraday Interval":
					//HIT,[Symbol],[Interval],[BeginDate BeginTime],[EndDate EndTime],[Reserved],[BeginFilterTime],[EndFilterTime],[DataDirection],[RequestID],[DatapointsPerSend],[IntervalType]<CR><LF> 
					//Retrieves interval data between [BeginDate BeginTime] and [EndDate EndTime] for the specified [Symbol].
					query.Append("HIT");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.CurrentSymbol);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.GetInterval);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.GetBeginDateTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.GetEndDateTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.Reserved);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.BeginFilterTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.EndFilterTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DataDirection);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.RequestID = "HIT");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DatapointsPerSend);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.IntervalType);
					query.Append(IQFeedCfg.Terminater);
					break;

				case "Daily Days":
					//HDX,[Symbol],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
					//Retrieves up to [MaxDatapoints] number of End-Of-Day Data for the specified [Symbol].
					query.Append("HDX");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.CurrentSymbol);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.MaxDatapoints);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DataDirection);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.RequestID = "HDX");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DatapointsPerSend);
					query.Append(IQFeedCfg.Terminater);
					break;

				case "Daily Interval":
					//HDT,[Symbol],[BeginDate],[EndDate],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
					//Retrieves Daily data between [BeginDate] and [EndDate] for the specified [Symbol].
					query.Append("HDT");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.CurrentSymbol);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.GetBeginDateTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.GetEndDateTime);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.MaxDatapoints);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DataDirection);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.RequestID = "HDT");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DatapointsPerSend);
					query.Append(IQFeedCfg.Terminater);
					break;

				case "Weekly Days":
				case "Weekly Interval":
					//HWX,[Symbol],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
					//Retrieves up to [MaxDatapoints] datapoints of composite weekly datapoints for the specified [Symbol].
					query.Append("HWX");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.CurrentSymbol);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.MaxDatapoints);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DataDirection);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.RequestID = "HWX");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DatapointsPerSend);
					query.Append(IQFeedCfg.Terminater);
					break;

				case "Monthly Days":
				case "Monthly Interval":
					//HMX,[Symbol],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
					//Retrieves up to [MaxDatapoints] datapoints of composite monthly datapoints for the specified [Symbol].
					query.Append("HMX");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.CurrentSymbol);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.MaxDatapoints);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DataDirection);
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.RequestID = "HMX");
					query.Append(IQFeedCfg.Delimiter);
					query.Append(r.DatapointsPerSend);
					query.Append(IQFeedCfg.Terminater);
					break;

				default:
					_logger.Error("[Request Error]");
					break;
			}

			requests.Add(query.ToString());

			return requests;
		}
	}
}
