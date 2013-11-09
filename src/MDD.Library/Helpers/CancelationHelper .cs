#region Usings

using System.Threading;

#endregion

namespace MDD.Library.Helpers
{
	public static class CancelationHelper
	{
		static CancelationHelper()
		{
			TokenSource = new CancellationTokenSource();
		}

		public static CancellationTokenSource TokenSource { get; private set; }

		public static void RearmTokenSource()
		{
			TokenSource = new CancellationTokenSource();
		}
	}
}
