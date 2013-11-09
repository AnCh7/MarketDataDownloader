#region Usings

using MDD.Library.Configuration;

#endregion

namespace MDD.Library.Models
{
	public class Parameters
	{
		public Parameters(string tbFolderText, string cbDTFormatDateText, string cbDTFormatDelimiterText,
						  string cbDTFormatTimeText, string cbDataDelimiterText, string cbSourceSelectedItem)
		{
			StorageFolder = tbFolderText;

			DateFormat = cbDTFormatDateText;
			DateTimeDelimiter = cbDTFormatDelimiterText;
			TimeFormat = cbDTFormatTimeText;

			if (cbDTFormatDelimiterText == Cfg.Space)
			{
				DateTimeDelimiter = Cfg.SpaceDelimiter;
			}

			if (cbDTFormatDelimiterText == Cfg.Tab)
			{
				DateTimeDelimiter = Cfg.TabDelimiter;
			}

			OutputDelimiter = cbDataDelimiterText;

			if (cbDataDelimiterText == Cfg.Space)
			{
				OutputDelimiter = Cfg.SpaceDelimiter;
			}

			if (cbDataDelimiterText == Cfg.Tab)
			{
				OutputDelimiter = Cfg.TabDelimiter;
			}

			DataFeedName = cbSourceSelectedItem;
		}

		public string StorageFolder { get; set; }

		public string OutputDelimiter { get; set; }

		public string DateFormat { get; set; }

		public string TimeFormat { get; set; }

		public string DateTimeDelimiter { get; set; }

		public string DataFeedName { get; set; }
	}
}
