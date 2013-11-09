#region Usings

using System;

#endregion

namespace MDD.DataFeed.IQFeed.Configuration
{
	public static class IQFeedCfg
	{
		public const string EndMessage = "ENDMSG";
		public const string NoData = "NO_DATA";
		public const string SyntaxError = "SYNTAX_ERROR";
		public const string InvalidSymbol = "Invalid symbol";

		public const string ProtocolRequest = "S,SET PROTOCOL,5.0\r\n";
		public const string ProtocolResponse = "CURRENT PROTOCOL";

		public const char Delimiter = ',';
		public const string EOL = "\0";

		public const int IQFeedLookup = 9100;
		public static int IQFeedLevel1 = 5009;
		public static int IQFeedLevel2 = 9200;
		public static int IQFeedAdmin = 9300;

		public static readonly string Terminater = Environment.NewLine;
	}
}
