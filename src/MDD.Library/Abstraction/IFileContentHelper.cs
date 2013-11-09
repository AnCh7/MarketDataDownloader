#region Usings

using System;
using System.IO;

#endregion

namespace MDD.Library.Abstraction
{
	public interface IFileContentHelper
	{
		DateTime? GetLastTimestampInFile(StreamWriter writer, char fieldDelimiter);
	}
}
