#region Usings

using System;
using System.Globalization;

using MDD.Library.Enums;
using MDD.Library.Helpers;

using NUnit.Framework;

#endregion

namespace MDD.UnitTests.Library.Helpers
{
	[TestFixture]
	public class DateTimeHelperTests
	{
		[Test]
		public void CompareDateTimeTest()
		{
			const string str1 = "2013-11-06 04:00:07.110";
			const string str2 = "2013-11-06 04:00:07.805";

			var dt1 = Convert.ToDateTime(str1, CultureInfo.InvariantCulture);
			var dt2 = Convert.ToDateTime(str2, CultureInfo.InvariantCulture);

			var result = DateTimeHelper.CompareDateTime(dt1, dt2);
			Assert.AreNotEqual(result, DateTimeCompare.Same);
		}
	}
}
