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
            if (string.IsNullOrEmpty(pString))
            {
                return string.Empty;
            }
            else
            {
                string withoutText = pString.Replace("\n", "")
                .Replace("\r", "");
                Console.WriteLine(withoutText);
                return withoutText;
            }
        }
    }
}
