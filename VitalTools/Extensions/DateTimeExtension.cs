using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTools.Extensions
{
	public static class DateTimeExtension
	{
		public static string ToDateString(this DateTime dateTime, bool internationalFormat = false)
		{
			if (internationalFormat)
				return $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}";
			else
				return $"{dateTime.Day}-{dateTime.Month}-{dateTime.Year}";
		}
	}
}
