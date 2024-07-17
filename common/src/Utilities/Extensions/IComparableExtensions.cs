using System;

namespace Blogposts.Common.Utilities.Extensions
{
    public static class IComparableExtensions
    {
        /// <summary>
        /// Determines whether the specified <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/> inclusive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/> inclusive; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBetween<T>(this T value, T min, T max)
            where T : IComparable<T>
        {
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }
    }
}