using System;
using System.Collections.Generic;
using System.Text;

namespace Vonage.Utility
{
    public static class Extension
    {
        public static bool HasLineBreaks(this string expression)
        {
            return expression.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Length > 1;
        }

        public static string RemoveCRLFFromString(this string pString)
        {
            //Return empty string if null passed
            return string.IsNullOrEmpty(pString) ? string.Empty : pString.Replace("\n", "").Replace("\r", "").Length > 9 ? pString.Substring(0, 9) : pString;
        }
    }
}
