#region Usings

using MDD.Library.Logging;

#endregion

namespace MDD.WinForms.Abstraction
{
	public interface IMainFormView
	{
		IMyLogger Logger { get; }

		void UpdateWithConnection(bool isConnected, string connectionMessage);
	}
}
