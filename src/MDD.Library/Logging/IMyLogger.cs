namespace MDD.Library.Logging
{
	public interface IMyLogger
	{
		void Error(string message);

		void Info(string message);

		void Warn(string message);
	}
}
