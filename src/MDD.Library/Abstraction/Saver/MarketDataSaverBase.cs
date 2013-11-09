#region Usings

using System;
using System.Collections;

using MDD.Library.Logging;

#endregion

namespace MDD.Library.Abstraction.Saver
{
	public abstract class MarketDataSaverBase
	{
		private object _sync = new object();

		protected MarketDataSaverBase(IPathHelper pathHelper, IMyLogger logger, IFileContentHelper fileContent)
		{
			if (pathHelper == null)
			{
				throw new ArgumentNullException("pathHelper");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			if (fileContent == null)
			{
				throw new ArgumentNullException("fileContent");
			}

			Logger = logger;
			PathHelper = pathHelper;
			FileContent = fileContent;
		}

		private IFileContentHelper FileContent { get; set; }

		private IPathHelper PathHelper { get; set; }

		protected IMyLogger Logger { get; private set; }

		protected DateTime? GetLastTimestampInFile(string storageFolder, string currentSymbol, char delimiter)
		{
			try
			{
				lock (_sync)
				{
					using (var writer = PathHelper.GetWriterToFile(storageFolder, currentSymbol))
					{
						return FileContent.GetLastTimestampInFile(writer, delimiter);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format("[GetLastTimestamp] {0}", ex.Message));
			}

			return null;
		}

		protected void WriteToFile(ArrayList data, string storageFolder, string currentSymbol)
		{
			try
			{
				lock (_sync)
				{
					using (var writer = PathHelper.GetWriterToFile(storageFolder, currentSymbol))
					{
						foreach (var d in data)
						{
							writer.WriteLine(d);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format("[WriteToFile] {0}", ex.Message));
			}
		}
	}
}
