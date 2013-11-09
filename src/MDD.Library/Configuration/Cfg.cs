#region Usings

using System;
using System.Configuration;

#endregion

namespace MDD.Library.Configuration
{
	public static class Cfg
	{
		public const string TxtExt = ".txt";
		public const string Slash = @"\";

		public const char CR = '\r';
		public const char LF = '\n';

		public const string Space = @"Space";
		public const string SpaceDelimiter = " ";

		public const string Tab = @"Tab";
		public const string TabDelimiter = "	";

		public const string Fidelity = "Fidelity";
		public const string IQFeed = "IQFeed";

		public const string NYSEOpenTime = "093000";
		public const string NYSECloseTime = "160000";

		public const string DayOpen = "000000";
		public const string DayClose = "235959";

		public static char[] SymbolsSeparator
		{
			get { return new[] {LF, ' '}; }
		}

		public static int UpdateInterval()
		{
			return Convert.ToInt32(ConfigurationManager.AppSettings["UpdateInterval"]);
		}

		public static int UpdateIntervalTick()
		{
			return Convert.ToInt32(ConfigurationManager.AppSettings["UpdateIntervalTick"]);
		}

		public static string Symbols()
		{
			return Convert.ToString(ConfigurationManager.AppSettings["Symbols"]);
		}

		public static string FolderForSaving()
		{
			return Convert.ToString(ConfigurationManager.AppSettings["FolderForSaving"]);
		}

		public static string AmountOfDays()
		{
			return Convert.ToString(ConfigurationManager.AppSettings["AmountOfDays"]);
		}

		public static int StreamWriterSavingInterval()
		{
			return Convert.ToInt32(ConfigurationManager.AppSettings["StreamWriterSavingInterval"]);
		}
	}
}
