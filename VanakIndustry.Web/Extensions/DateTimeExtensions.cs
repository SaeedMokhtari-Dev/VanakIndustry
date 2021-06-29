using System;
using System.Globalization;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Web.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToPersianDateTime(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return
                $"{persianCalendar.GetYear(dateTime)}/" +
                $"{persianCalendar.GetMonth(dateTime).ToString().PadLeft(2, '0')}/" +
                $"{persianCalendar.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0')}" +
                $" {persianCalendar.GetHour(dateTime).ToString().PadLeft(2, '0')}" +
                $":{persianCalendar.GetMinute(dateTime).ToString().PadLeft(2, '0')}" +
                $":{persianCalendar.GetSecond(dateTime).ToString().PadLeft(2, '0')}";
        }
        public static DateTime ToGregorianDateTime(this string persianDateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            DateTime dateTime = DateTime.ParseExact(persianDateTime, DateTimeConstants.PersianDateTimeFormat, CultureInfo.InvariantCulture);
            return persianCalendar.ToDateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute,
                dateTime.Second, 0);
        }
    }
}