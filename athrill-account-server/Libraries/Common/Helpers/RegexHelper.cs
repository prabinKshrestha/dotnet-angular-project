using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AT.Common.Helpers
{
    public static class RegexHelper
    {
        public static string Password = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

        public static string Slug = @"^[a-zA-Z0-9]*[^!@%~?:#|$%&()0{},*;:<>.`'""\[\] +\\\/]*$";
        public static bool MatchRegex<T>(this T value, string regexString)
            where T : class
        {
            Regex regex = new Regex(regexString);
            return regex.IsMatch(value.ToString());
        }
    }
}
