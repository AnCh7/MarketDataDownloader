#region Usings

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Xml;

using MDD.DataFeed.Fidelity.Models;
using MDD.Library.Abstraction;
using MDD.Library.Abstraction.Parser;
using MDD.Library.Helpers;
using MDD.Library.Logging;

#endregion

namespace MDD.DataFeed.Fidelity.DataParser
{
	public class FidelityDataParser : IMarketDataParser
	{
		private readonly IMyLogger _logger;

		public FidelityDataParser(IMyLogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}

			_logger = logger;
		}

		public async Task StartParsingAsync(RequestBase parameters)
		{
			var p = (RequestFidelity)parameters;

			try
			{
				while (await DataflowHelper.ResponsesQueue.OutputAvailableAsync(CancelationHelper.TokenSource.Token))
				{
					if (CancelationHelper.TokenSource.Token.IsCancellationRequested)
					{
						return;
					}

					var response = await DataflowHelper.ResponsesQueue.ReceiveAsync(CancelationHelper.TokenSource.Token);

					if (!string.IsNullOrEmpty(response))
					{
						var document = new XmlDocument();
						document.LoadXml(response);

						var marketdata = ParseToMarketData(document, p.IsIntraday, p.Interval);

						foreach (var m in marketdata)
						{
							DataflowHelper.MarketDataQueue.Post(m);
						}
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

		private IEnumerable<FidelityMarketData> ParseToMarketData(XmlNode document, bool isIntraday, int interval)
		{
			var marketData = new List<FidelityMarketData>();
			return marketData;
		}

		private List<FidelityMarketData> UniteAllToMarketData(string strDates, string srtOpen, string strHigh, string strLow,
															  string strClose, string strVolume, bool isIntraday, int interval)
		{
			var list = new List<FidelityMarketData>();

			if (!string.IsNullOrEmpty(strDates) && !string.IsNullOrEmpty(strClose))
			{}

			return list;
		}

		private string GetValue(string originalValue, string replacementValue, ref int counter)
		{
			string result = string.Empty;
			return result;
		}

		private string ReadDate(bool isIntraday, int interval, string strDates, ref int counterDate)
		{
			string result = string.Empty;
			return result;
		}

		private string ReadValueFromXmlSet(string set, ref int counter)
		{
			string result = string.Empty;
			return result;
		}

		private string ComposeErrorMessage(XmlNode symbolNode)
		{
			string result = string.Empty;
			return result;
		}
	}
}
