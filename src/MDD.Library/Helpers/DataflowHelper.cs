#region Usings

using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using MDD.Library.Abstraction;

#endregion

namespace MDD.Library.Helpers
{
	public static class DataflowHelper
	{
		static DataflowHelper()
		{
			MarketDataQueue = new BufferBlock<MarketDataBase>();
			ResponsesQueue = new BufferBlock<string>();
		}

		public static BufferBlock<MarketDataBase> MarketDataQueue { get; private set; }

		public static BufferBlock<string> ResponsesQueue { get; private set; }

		public static Task ResponsesQueueCompletion
		{
			get { return ResponsesQueue.Completion; }
		}

		public static Task MarketDataQueueCompletion
		{
			get { return MarketDataQueue.Completion; }
		}

		public static void RearmDataflowQueues()
		{
			MarketDataQueue = new BufferBlock<MarketDataBase>();
			ResponsesQueue = new BufferBlock<string>();
		}
	}
}
