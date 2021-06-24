using System;

namespace VanakIndustry.Web.Extensions
{
    public static class StringExtensions
    {
        public static string ReverseDate(this string s)
        {
            var dateSplit = s.Split("/");
            Array.Reverse(dateSplit);
            return String.Join("/", dateSplit);
        }
    }
}