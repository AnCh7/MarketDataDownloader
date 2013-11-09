#region Usings

using System.Collections.Generic;

#endregion

namespace MDD.Library.Abstraction
{
	public interface IRequestBuilder
	{
		List<string> CreateRequests(RequestBase r);
	}
}
