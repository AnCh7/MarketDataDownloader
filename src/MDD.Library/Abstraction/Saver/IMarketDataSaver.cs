#region Usings

using System.Threading.Tasks;

using MDD.Library.Models;

#endregion

namespace MDD.Library.Abstraction.Saver
{
	public interface IMarketDataSaver
	{
		Task StartSavingAsync(Parameters parameters, RequestBase request);
	}
}
