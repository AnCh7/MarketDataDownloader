#region Usings

using System.IO;

#endregion

namespace MDD.Library.Abstraction
{
	public interface IPathHelper
	{
		bool CreateDirectory(string folder);

		StreamWriter GetWriterToFile(string folder, string symbol);
	}
}
