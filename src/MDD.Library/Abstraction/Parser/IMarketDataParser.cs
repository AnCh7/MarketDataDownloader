#region Usings

using System.Threading.Tasks;

#endregion

namespace MDD.Library.Abstraction.Parser
{
	public interface IMarketDataParser
	{
		Task StartParsingAsync(RequestBase parameters);
	}
}
