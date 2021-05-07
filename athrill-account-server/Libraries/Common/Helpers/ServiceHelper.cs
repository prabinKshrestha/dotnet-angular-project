using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AT.Common.Helpers
{
    public static class ServiceHelper
    {
        private static List<string> nonElementOfSlug = new List<string>() {
            "&",",","(",")","\\","/","'","\"","`","~","+","*",".","?","<",">",":",";","{","}","[","]","|","!","#","$","%","^","&","_"
        };

        public static string MakeSlug(string title)
        {
            StringBuilder slugifyObj = new StringBuilder(title);
            nonElementOfSlug.ForEach(x => slugifyObj.Replace(x, ""));
            slugifyObj.Replace(" ", "-");
            Regex regex = new Regex("-+");
            return regex.Replace(slugifyObj.ToString().ToLower(), "-");
        }

        public static string GetDisplayName(this System.Enum enumType)
        {
            return enumType.GetType().GetMember(enumType.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .Name;
        }
    }
}
