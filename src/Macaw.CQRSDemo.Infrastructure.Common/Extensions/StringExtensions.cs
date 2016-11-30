using System;
using System.Linq;

namespace Macaw.CQRSDemo.Infrastructure.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsAny(this string thestring, params string[] tokens)
        {
            return tokens.Any(thestring.Contains);
        }

        public static bool EqualsAny(this string thestring, params string[] tokens)
        {
            return tokens.Any(token => thestring.Equals(token, StringComparison.OrdinalIgnoreCase));
        }

        public static Uri ToUri(this string url, UriKind kind = UriKind.RelativeOrAbsolute)
        {
            return new Uri(url, kind);
        }

        public static DateTime? ToDate(this string thestring, DateTime? defaultDate = null)
        {
            DateTime date;
            var success = DateTime.TryParse(thestring, out date);
            return success ? date : defaultDate;
        }

        public static bool ToBool(this string thestring)
        {
            bool value;
            var success = bool.TryParse(thestring, out value);
            return success && value;
        }

        public static int ToInt(this string thestring, int defaultValue = 0)
        {
            int number;
            var success = int.TryParse(thestring, out number);
            return success ? number : defaultValue;
        }

        public static bool IsNullOrWhitespace(this string thestring)
        {
            return string.IsNullOrWhiteSpace(thestring);
        }

        public static bool IsAlphaNumeric(this string thestring)
        {
            return string.IsNullOrWhiteSpace(thestring);
        }
    }

}