using System;

namespace LunchTime.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string original, string toCompare)
        {
            return original.Equals(toCompare, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}