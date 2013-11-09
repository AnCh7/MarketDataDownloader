#region Usings

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using MDD.DataFeed.IQFeed.Configuration;
using MDD.DataFeed.IQFeed.Models;
using MDD.Library.Abstraction;
using MDD.Library.Abstraction.Parser;
using MDD.Library.Configuration;
using MDD.Library.Helpers;
using MDD.Library.Logging;

#endregion

namespace MDD.DataFeed.IQFeed.DataParser
{
	public class IQFeedDataParser : IMarketDataParser
	{
		private readonly IMyLogger _logger;

		public IQFeedDataParser(IMyLogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}

			_logger = logger;
		}

		public async Task StartParsingAsync(RequestBase parameters)
		{
			var p = (RequestIQFeed)parameters;

			try
			{
				var previousLastElement = string.Empty;

				while (await DataflowHelper.ResponsesQueue.OutputAvailableAsync(CancelationHelper.TokenSource.Token))
				{
					if (CancelationHelper.TokenSource.Token.IsCancellationRequested)
					{
						return;
					}

					var response = await DataflowHelper.ResponsesQueue.ReceiveAsync(CancelationHelper.TokenSource.Token);
					var newArray = CombineResponses(response, ref previousLastElement);

					foreach (var item in newArray)
					{
						if (!IsValidResponse(item))
						{
							return;
						}

						var marketData = ParseToMarketData(item);
						DataflowHelper.MarketDataQueue.Post(marketData);
					}
				}
			}
			catch (TaskCanceledException)
			{}
			catch (Exception ex)
			{
				_logger.Error(string.Format("[Parsing] {0}", ex.Message));
			}
			finally
			{
				DataflowHelper.MarketDataQueue.Complete();
			}
		}

		private IEnumerable<string> CombineResponses(string response, ref string previousLastElement)
		{
			var cleared = response.Replace(IQFeedCfg.EOL, string.Empty);

			// Create arrays
			var originalArray = cleared.Split(Cfg.LF);
			var newArray = new string[originalArray.Length - 1];

			// Save and delete first element. Write it to new array
			var firstElement = originalArray[0];
			originalArray = originalArray.Skip(1).ToArray();
			newArray[0] = previousLastElement + firstElement;

			// Save last element. If it partial - delete it
			var lastElement = originalArray.Last();
			previousLastElement = lastElement;
			originalArray = originalArray.Take(originalArray.Length - 1).ToArray();

			// Copy all from source array, dont rewrite first in new array, cos we write it already.
			Array.Copy(originalArray, 0, newArray, 1, originalArray.Length);
			return newArray;
		}

		private IQFeedMarketData ParseToMarketData(string response)
		{
			var marketData = new IQFeedMarketData();

			var str = response.TrimEnd(Cfg.CR);
			var splitted = str.Split(new[] {IQFeedCfg.Delimiter}, StringSplitOptions.RemoveEmptyEntries);

			var requestType = splitted[0];
			if (!string.IsNullOrEmpty(requestType))
			{
				switch (requestType)
				{
					case "HTT":
					case "HTD":
					case "HTX":
						marketData = ConvertFromTickData(splitted);
						break;

					case "HIT":
					case "HID":
					case "HIX":
						marketData = ConvertFromIntradayData(splitted);
						break;

					case "HMX":
					case "HWX":
					case "HDT":
					case "HDX":
						marketData = ConvertFromDailyData(splitted);
						break;

					default:
						_logger.Error("[Unknown RequestId]");
						break;
				}
			}
			else
			{
				_logger.Error("[Empty RequestId]");
			}

			return marketData;
		}

		private bool IsValidResponse(string response)
		{
			if (string.IsNullOrEmpty(response))
			{
				_logger.Error("[Empty Response]");
				return false;
			}
			if (response.Contains(IQFeedCfg.EndMessage))
			{
				return false;
			}
			if (response.Contains(IQFeedCfg.SyntaxError))
			{
				_logger.Error("[Syntax Error]");
				return false;
			}
			if (response.Contains(IQFeedCfg.NoData))
			{
				return false;
			}
			if (response.Contains(IQFeedCfg.ProtocolResponse))
			{
				return false;
			}
			if (response.Contains(IQFeedCfg.InvalidSymbol))
			{
				_logger.Error("[Invalid Symbol]");
				return false;
			}

			return true;
		}

		private IQFeedMarketData ConvertFromTickData(IList<string> input)
		{
			if (input.Count != 11)
			{
				_logger.Error("[TickData wrong count]");
				return null;
			}

			var marketData = new IQFeedMarketData();

			marketData.RequestType = input[0];
			marketData.TimeStamp = Convert.ToDateTime(input[1], CultureInfo.InvariantCulture);
			marketData.Last = input[2];
			marketData.LastSize = input[3];
			marketData.TotalVolume = input[4];
			marketData.Bid = input[5];
			marketData.Ask = input[6];
			marketData.TickId = input[7];
			marketData.BasisForLast = input[8];
			marketData.TradeMarketCenter = input[9];
			marketData.TradeConditions = input[10];

			return marketData;
		}

		private IQFeedMarketData ConvertFromIntradayData(IList<string> input)
		{
			if (input.Count != 8)
			{
				_logger.Error("[IntradayData wrong count]");
				return null;
			}

			var marketData = new IQFeedMarketData();

			marketData.RequestType = input[0];
			marketData.TimeStamp = Convert.ToDateTime(input[1], CultureInfo.InvariantCulture);
			marketData.High = input[2];
			marketData.Low = input[3];
			marketData.Open = input[4];
			marketData.Close = input[5];
			marketData.TotalVolume = input[6];
			marketData.PeriodVolume = input[7];

			return marketData;
		}

		private IQFeedMarketData ConvertFromDailyData(IList<string> input)
		{
			if (input.Count != 8)
			{
				_logger.Error("[DailyData wrong count]");
				return null;
			}

			var marketData = new IQFeedMarketData();

			marketData.RequestType = input[0];
			marketData.TimeStamp = Convert.ToDateTime(input[1], CultureInfo.InvariantCulture);
			marketData.High = input[2];
			marketData.Low = input[3];
			marketData.Open = input[4];
			marketData.Close = input[5];
			marketData.PeriodVolume = input[6];
			marketData.OpenInterest = input[7];

			return marketData;
		}
	}
}
