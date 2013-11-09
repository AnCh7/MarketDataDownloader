#region Usings

using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using MDD.DataFeed.IQFeed.Configuration;
using MDD.DataFeed.IQFeed.Models;
using MDD.Library.Abstraction;
using MDD.Library.Abstraction.Saver;
using MDD.Library.Configuration;
using MDD.Library.Enums;
using MDD.Library.Helpers;
using MDD.Library.Logging;
using MDD.Library.Models;

#endregion

namespace MDD.DataFeed.IQFeed.DataSaver
{
	public class IQFeedFileSaver : MarketDataSaverBase, IMarketDataSaver
	{
		public IQFeedFileSaver(IPathHelper pathHelper, IMyLogger logger, IFileContentHelper fileContent)
			: base(pathHelper, logger, fileContent)
		{}

		public async Task StartSavingAsync(Parameters parameters, RequestBase request)
		{
			var p = (RequestIQFeed)request;
			var marketDataList = new ArrayList();

			try
			{
				var lastTimeStamp = base.GetLastTimestampInFile(parameters.StorageFolder, p.CurrentSymbol, IQFeedCfg.Delimiter);

				while (await DataflowHelper.MarketDataQueue.OutputAvailableAsync(CancelationHelper.TokenSource.Token))
				{
					if (CancelationHelper.TokenSource.Token.IsCancellationRequested)
					{
						return;
					}

					var marketData = await DataflowHelper.MarketDataQueue.ReceiveAsync(CancelationHelper.TokenSource.Token);
					var md = (IQFeedMarketData)marketData;

					if (md == null)
					{
						return;
					}

					var compareDateTime = DateTimeHelper.CompareDateTime(lastTimeStamp, md.TimeStamp);
					if (compareDateTime == DateTimeCompare.Later || compareDateTime == DateTimeCompare.NewFile)
					{
						switch (md.RequestType)
						{
							case "HTX":
							case "HTD":
							case "HTT":
								AddTickData(md, parameters, marketDataList);
								break;

							case "HIX":
							case "HID":
							case "HIT":
								AddIntradayData(md, parameters, marketDataList);
								break;

							case "HDX":
							case "HDT":
							case "HWX":
							case "HMX":
								AddDailyData(md, parameters, marketDataList);
								break;

							default:
								base.Logger.Error("[Unknown RequestId]");
								break;
						}

						if (marketDataList.Count == Cfg.StreamWriterSavingInterval())
						{
							base.WriteToFile(marketDataList, parameters.StorageFolder, p.CurrentSymbol);
							marketDataList.Clear();
						}
					}
				}
			}
			catch (TaskCanceledException)
			{}
			catch (Exception ex)
			{
				base.Logger.Error(string.Format("[StartSavingAsync] {0}", ex.Message));
			}
			finally
			{
				base.WriteToFile(marketDataList, parameters.StorageFolder, p.CurrentSymbol);
			}
		}

		private void AddTickData(IQFeedMarketData marketData, Parameters parameters, ArrayList marketDataList)
		{
			var b = new StringBuilder();

			b.Append(marketData.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff"));
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.Last);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.LastSize);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.TotalVolume);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.Bid);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.Ask);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.TickId);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.BasisForLast);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.TradeMarketCenter);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.TradeConditions);

			marketDataList.Add(b.ToString());
		}

		private void AddIntradayData(MarketDataBase marketData, Parameters parameters, ArrayList marketDataList)
		{
			var b = new StringBuilder();

			b.Append(marketData.TimeStamp.ToString(parameters.DateFormat + parameters.DateTimeDelimiter + parameters.TimeFormat));
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.Open);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.High);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.Low);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.Close);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.TotalVolume);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.PeriodVolume);

			marketDataList.Add(b.ToString());
		}

		private void AddDailyData(MarketDataBase marketData, Parameters parameters, ArrayList marketDataList)
		{
			var b = new StringBuilder();

			b.Append(marketData.TimeStamp.ToString(parameters.DateFormat));
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.Open);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.High);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.Low);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.Close);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.PeriodVolume);
			b.Append(parameters.OutputDelimiter);
			b.Append(marketData.OpenInterest);

			marketDataList.Add(b.ToString());
		}
	}
}
