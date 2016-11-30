using System;

namespace Macaw.CQRSDemo.Infrastructure.Common.Extensions
{
    public static class NumberExtensions
    {
        public static bool BetweenWith(this int number, int lower, int upper)
        {
            return number >= lower && number <= upper;
        }

        public static int Increment(this int number, int max = 99)
        {
            var temp = number + 1;
            return temp <= max ? temp : number;
        }

        public static string ToDefaultIfNegativeOrZero(this int number)
        {
            return number <= 0 ? string.Empty : number.ToString();
        }
    }
}