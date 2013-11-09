#region Usings

using MDD.DataFeed.IQFeed.Models;
using MDD.Library.Abstraction;
using MDD.Library.Abstraction.Client;
using MDD.Library.Abstraction.Manager;
using MDD.Library.Abstraction.Parser;
using MDD.Library.Abstraction.Saver;
using MDD.Library.Models;
using MDD.UnitTests.Resolver;

using Microsoft.Practices.Unity;

using Newtonsoft.Json;

using NUnit.Framework;

#endregion

namespace MDD.UnitTests.IQFeed
{
	[TestFixture]
	public sealed class IQFeedManagerTests
	{
		private readonly IDataFeedManager _manager;
		private bool _isConnected;
		private string _connectionMessage;
		private bool _eventFired = false;

		public IQFeedManagerTests()
		{
			_manager = DependencyFactory.Container.Resolve<IDataFeedManager>("IQFeed",
																			 new ParameterOverrides
																			 {
																				 {"client", DependencyFactory.Container.Resolve<IDataFeedClient>("IQFeed")},
																				 {"saver", DependencyFactory.Container.Resolve<IMarketDataSaver>("IQFeed")},
																				 {"parser", DependencyFactory.Container.Resolve<IMarketDataParser>("IQFeed")},
																				 {"requestBuilder", DependencyFactory.Container.Resolve<IRequestBuilder>("IQFeed")}
																			 });
		}

		private void UpdateWithConnection(bool isConnected, string connectionMessage)
		{
			_eventFired = true;
			_isConnected = isConnected;
			_connectionMessage = connectionMessage;
		}

		[Test]
		public void TestConnectionCallbackFalse()
		{
			_manager.ConnectionEstablished = UpdateWithConnection;

			_manager.StartService();
			while (true)
			{
				if (_eventFired)
				{
					_eventFired = false;
					break;
				}
			}

			_manager.StopService();
			while (true)
			{
				if (_eventFired)
				{
					break;
				}
			}

			Assert.AreEqual(_isConnected, false);
			Assert.AreEqual(_connectionMessage, "Disconnected");
		}

		[Test]
		public void TestConnectionCallbackTrue()
		{
			_manager.ConnectionEstablished = UpdateWithConnection;
			_manager.StartService();

			while (true)
			{
				if (_eventFired)
				{
					break;
				}
			}

			Assert.AreEqual(_isConnected, true);
			Assert.AreEqual(_connectionMessage, "Socket connected to 127.0.0.1:9100");
		}

		[Test]
		public void TestDownloading_Days_1Min()
		{
			const string r = @"{
								  ""MaxDatapoints"": ""0"",
								  ""DataDirection"": ""1"",
								  ""DatapointsPerSend"": ""2500"",
								  ""IntervalType"": ""s"",
								  ""Reserved"": """",
								  ""RequestID"": """",
								  ""Symbols"": [
									""AAPL"",
									""TSLA""
								  ],
								  ""CurrentSymbol"": ""AAPL"",
								  ""BeginDateTime"": ""2013-11-05 10:00:00"",
								  ""EndDateTime"": ""2013-11-06 10:00:00"",
								  ""BeginFilterTime"": ""000000"",
								  ""EndFilterTime"": ""235959"",
								  ""TimeFrameName"": ""Intraday"",
								  ""TimeFrameType"": ""Days"",
								  ""Interval"": 1,
								  ""AmountOfDays"": ""1""
								}";

			const string p = @"{
								  ""StorageFolder"": ""c:\\MarketData\\"",
								  ""OutputDelimiter"": "","",
								  ""DateFormat"": ""yyyy-MM-dd"",
								  ""TimeFormat"": ""H:mm:ss"",
								  ""DateTimeDelimiter"": "" "",
								  ""DataFeedName"": ""IQFeed""
								}";

			var request = JsonConvert.DeserializeObject<RequestIQFeed>(r);
			var parameters = JsonConvert.DeserializeObject<Parameters>(p);

			//_manager.StartService();
			//_manager.DownloadMarketData(request, parameters);

			//Assert.AreEqual(_isConnected, true);
			//Assert.AreEqual(_connectionMessage, "Connected");
		}
	}
}
