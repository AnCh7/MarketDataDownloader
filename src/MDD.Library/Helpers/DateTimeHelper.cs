#region Usings

using System;
using System.Collections.Generic;

using MDD.Library.Enums;

#endregion

namespace MDD.Library.Helpers
{
	public static class DateTimeHelper
	{
		private static DateTime _openTime = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month,
														 DateTime.Now.Date.Day, 9, 30, 0);

		private static DateTime _closeTime = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month,
														  DateTime.Now.Date.Day, 0x10, 0, 0);

		public static DateTime Date2004 = new DateTime(2004, 1, 1);

		public static DateTime Date1971 = new DateTime(1971, 1, 1);

		public static DateTime NowEST = ConvertLocalToEST(DateTime.Now);

		private static bool NotCloseTime(DateTime dt)
		{
			return ((dt.Hour <= _closeTime.Hour) && ((dt.Hour != _closeTime.Hour) || (dt.Minute <= _closeTime.Minute)));
		}

		private static int Subtract(DateTime beginDateTime, DateTime endDateTime)
		{
			return (endDateTime.Subtract(beginDateTime)).Days;
		}

		private static string FidelityFormat(DateTime dt)
		{
			return dt.ToString("yyyy/MM/dd-HH:mm:ss");
		}

		public static string ApplyFormat(DateTime dt, string dateFormat, string delimiter, string timeFormat,
										 string timeFrameName)
		{
			var format = dateFormat + delimiter + timeFormat;

			string parseDateTime;
			if (timeFrameName != "Intraday")
			{
				parseDateTime = dt.ToString(dateFormat);
			}
			else
			{
				parseDateTime = dt.ToString(format);
			}

			return parseDateTime;
		}

		private static bool NotOpenTime(DateTime dt)
		{
			return ((dt.Hour >= _openTime.Hour) && ((dt.Hour != _openTime.Hour) || (dt.Minute > _openTime.Minute)));
		}

		private static bool NotWeekend(DateTime dt)
		{
			return ((dt.DayOfWeek != DayOfWeek.Saturday) && (dt.DayOfWeek != DayOfWeek.Sunday));
		}

		public static DateTime ConvertLocalToEST(DateTime localDateTime)
		{
			var utcDt = TimeZoneInfo.ConvertTimeToUtc(localDateTime);
			return TimeZoneInfo.ConvertTimeFromUtc(utcDt, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
		}

		public static DateTime SubstractOneMinute(DateTime dateTime, bool isFidelity)
		{
			DateTime result;
			if (isFidelity)
			{
				result = dateTime.AddMinutes(-15);
			}
			else
			{
				result = dateTime;
			}

			var roundDowned = new DateTime((result.Ticks / TimeSpan.FromMinutes(1).Ticks) * TimeSpan.FromMinutes(1).Ticks);
			return roundDowned;
		}

		private static IEnumerable<Tuple<DateTime, DateTime>> SplitDateRange(DateTime start, DateTime end, int dayChunkSize)
		{
			var startOfThisPeriod = start;
			while (startOfThisPeriod < end)
			{
				var endOfThisPeriod = startOfThisPeriod.AddDays(dayChunkSize);
				endOfThisPeriod = endOfThisPeriod < end ? endOfThisPeriod : end;
				yield return Tuple.Create(startOfThisPeriod, endOfThisPeriod);
				startOfThisPeriod = endOfThisPeriod;
			}
		}

		public static DateTime FromUnixTime(int unixTime)
		{
			var dt = new DateTime(1970, 1, 1, 0, 0, 0) + new TimeSpan(unixTime * 0x989680L);

			var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
			var easternTime = TimeZoneInfo.ConvertTimeFromUtc(dt, easternZone);

			return easternTime;
		}

		public static DateTimeCompare CompareDateTime(DateTime? date1, DateTime date2)
		{
			if (!date1.HasValue)
			{
				return DateTimeCompare.NewFile;
			}

			var result = DateTime.Compare(date2, date1.Value);

			if (result < 0)
			{
				return DateTimeCompare.Earlier;
			}
			if (result == 0)
			{
				return DateTimeCompare.Same;
			}

			return DateTimeCompare.Later;
		}

		public static bool CheckFidelityValidDateTime(bool extendedMarket, bool isIntraday, DateTime dt)
		{
			return ((extendedMarket || (!isIntraday)) || (NotOpenTime(dt) && NotCloseTime(dt))) && NotWeekend(dt);
		}

		public static void SplitDatesByChunkForFidelity(string timeFrameName, int chunkSize, DateTime beginDateTime,
														DateTime endDateTime, Dictionary<string, string> dates)
		{
			if (Subtract(beginDateTime, endDateTime) > chunkSize && timeFrameName == "Intraday")
			{
				var datesSplitted = SplitDateRange(beginDateTime, endDateTime, chunkSize);

				foreach (var d in datesSplitted)
				{
					dates.Add(FidelityFormat(d.Item1), FidelityFormat(d.Item2));
				}
			}
			else
			{
				dates.Add(FidelityFormat(beginDateTime), FidelityFormat(endDateTime));
			}
		}
	}
}
