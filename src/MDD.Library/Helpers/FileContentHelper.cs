#region Usings

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using MDD.Library.Abstraction;
using MDD.Library.Logging;

#endregion

namespace MDD.Library.Helpers
{
	public class FileContentHelper : IFileContentHelper
	{
		private readonly IMyLogger _logger;

		public FileContentHelper(IMyLogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}

			_logger = logger;
		}

		public DateTime? GetLastTimestampInFile(StreamWriter writer, char fieldDelimiter)
		{
			var path = ((FileStream)(writer.BaseStream)).Name;

			var isFileExist = File.Exists(path);
			var isNonEmptyFile = new FileInfo(path).Length > 0;

			if (isFileExist && isNonEmptyFile)
			{
				var reader = new ReverseLineReader(path, Encoding.UTF8);
				var lastLine = reader.Take(1).ToArray()[0];

				try
				{
					var splitted = lastLine.Split(fieldDelimiter);
					return Convert.ToDateTime(splitted[0], CultureInfo.InvariantCulture);
				}
				catch (Exception ex)
				{
					_logger.Error(string.Format("[CheckExistingData] {0}", ex.Message));
				}
			}

			return null;
		}
	}
}
