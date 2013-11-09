#region Usings

using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using MDD.DataFeed.Fidelity.Configuration;
using MDD.DataFeed.Fidelity.Models;
using MDD.Library.Abstraction;
using MDD.Library.Abstraction.Saver;
using MDD.Library.Configuration;
using MDD.Library.Enums;
using MDD.Library.Helpers;
using MDD.Library.Logging;
using MDD.Library.Models;

#endregion

namespace MDD.DataFeed.Fidelity.DataSaver
{
	public class FidelityFileSaver : MarketDataSaverBase, IMarketDataSaver
	{
		public FidelityFileSaver(IPathHelper pathHelper, IMyLogger logger, IFileContentHelper fileContent)
			: base(pathHelper, logger, fileContent)
		{}

		public async Task StartSavingAsync(Parameters parameters, RequestBase requestFidelity)
		{
			var p = (RequestFidelity)requestFidelity;
			var marketDataList = new ArrayList();

			try
			{
				var lastTimeStamp = base.GetLastTimestampInFile(parameters.StorageFolder, p.CurrentSymbol, FidelityCfg.Delimiter);

				while (await DataflowHelper.MarketDataQueue.OutputAvailableAsync(CancelationHelper.TokenSource.Token))
				{
					if (CancelationHelper.TokenSource.Token.IsCancellationRequested)
					{
						return;
					}

					var marketData = await DataflowHelper.MarketDataQueue.ReceiveAsync(CancelationHelper.TokenSource.Token);
					var md = (FidelityMarketData)marketData;

					if (md == null)
					{
						return;
					}

					var compareDateTime = DateTimeHelper.CompareDateTime(lastTimeStamp, md.TimeStamp);
					if (compareDateTime == DateTimeCompare.Later || compareDateTime == DateTimeCompare.NewFile)
					{
						var isValidDateTime = DateTimeHelper.CheckFidelityValidDateTime(p.ExtendedMarket, p.IsIntraday, md.TimeStamp);
						if (isValidDateTime)
						{
							var dateTime = DateTimeHelper.ApplyFormat(md.TimeStamp, parameters.DateFormat, parameters.DateTimeDelimiter,
																	  parameters.TimeFormat, p.TimeFrameName);

							AddToArray(parameters, dateTime, md, marketDataList);

							if (marketDataList.Count == Cfg.StreamWriterSavingInterval())
							{
								base.WriteToFile(marketDataList, parameters.StorageFolder, p.CurrentSymbol);
								marketDataList.Clear();
							}
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

		private void AddToArray(Parameters parameters, string dateTime, FidelityMarketData fmd, ArrayList marketDataList)
		{
			var b = new StringBuilder();

			b.Append(dateTime);
			b.Append(parameters.OutputDelimiter);
			b.Append(fmd.Open);
			b.Append(parameters.OutputDelimiter);
			b.Append(fmd.High);
			b.Append(parameters.OutputDelimiter);
			b.Append(fmd.Low);
			b.Append(parameters.OutputDelimiter);
			b.Append(fmd.Close);
			b.Append(parameters.OutputDelimiter);
			b.Append(fmd.Volume);

			marketDataList.Add(b.ToString());
		}
	}
}
