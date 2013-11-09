#region Usings

using NLog;
using NLog.Config;

#endregion

namespace MDD.Library.Logging
{
	public class MyLogger : IMyLogger
	{
		private readonly Logger _log;

		public MyLogger(LoggingConfiguration configuration, string className)
		{
			LogManager.Configuration = configuration;
			_log = LogManager.GetLogger(className);
		}

		public void Error(string message)
		{
			_log.Error(message);
		}

		public void Info(string message)
		{
			_log.Info(message);
		}

		public void Warn(string message)
		{
			_log.Warn(message);
		}
	}
}
