using System;
using System.Globalization;

namespace VanakIndustry.Web.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToPersianDateTime(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return
                $"{persianCalendar.GetYear(dateTime)}/{persianCalendar.GetMonth(dateTime)}/{persianCalendar.GetDayOfMonth(dateTime)}" +
                $"-{persianCalendar.GetHour(dateTime)}:{persianCalendar.GetMinute(dateTime)}:{persianCalendar.GetSecond(dateTime)}";
        }
    }
}