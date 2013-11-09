#region Usings

using System;

using MDD.Library.Abstraction.Client;
using MDD.Library.Abstraction.Parser;
using MDD.Library.Abstraction.Saver;

#endregion

namespace MDD.Library.Abstraction.Manager
{
	public abstract class DataFeedManagerBase
	{
		protected DataFeedManagerBase(IDataFeedClient client, IMarketDataSaver saver, IMarketDataParser parser,
									  IRequestBuilder requestBuilder)
		{
			if (client == null)
			{
				throw new ArgumentNullException("client");
			}
			if (saver == null)
			{
				throw new ArgumentNullException("saver");
			}
			if (parser == null)
			{
				throw new ArgumentNullException("parser");
			}
			if (requestBuilder == null)
			{
				throw new ArgumentNullException("requestBuilder");
			}

			Client = client;
			RequestBuilder = requestBuilder;
			Parser = parser;
			Saver = saver;
		}

		protected IDataFeedClient Client { get; private set; }

		protected IMarketDataParser Parser { get; private set; }

		protected IRequestBuilder RequestBuilder { get; private set; }

		protected IMarketDataSaver Saver { get; private set; }
	}
}
