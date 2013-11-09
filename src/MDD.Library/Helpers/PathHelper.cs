#region Usings

using System;
using System.Globalization;
using System.IO;

using MDD.Library.Abstraction;
using MDD.Library.Configuration;
using MDD.Library.Logging;

#endregion

namespace MDD.Library.Helpers
{
	public sealed class PathHelper : IPathHelper
	{
		private readonly IMyLogger _logger;

		public PathHelper(IMyLogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}

			_logger = logger;
		}

		public bool CreateDirectory(string folder)
		{
			if (string.IsNullOrEmpty(folder))
			{
				_logger.Error("[Empty folder]");
				return false;
			}

			var successfulCreating = Directory.Exists(folder);

			if (IsValidPath(folder))
			{
				Directory.CreateDirectory(folder);
				successfulCreating = true;
			}

			return successfulCreating;
		}

		public StreamWriter GetWriterToFile(string folder, string symbol)
		{
			var quotesfilepath = GetFilePath(folder, symbol);
			var fs = new FileStream(quotesfilepath, FileMode.Append, FileAccess.Write);
			var stream = new StreamWriter(fs) {AutoFlush = false};

			return stream;
		}

		private string GetFilePath(string folder, string symbol)
		{
			var quotesfilepath = folder + Cfg.Slash + symbol + Cfg.TxtExt;
			return quotesfilepath;
		}

		private bool IsValidPath(string folder)
		{
			var invalidPathChars = Path.GetInvalidPathChars();

			var isValidPath = true;

			foreach (var symbol in invalidPathChars)
			{
				if (folder.Contains(symbol.ToString(CultureInfo.InvariantCulture)))
				{
					isValidPath = false;
				}
			}

			return isValidPath;
		}
	}
}
