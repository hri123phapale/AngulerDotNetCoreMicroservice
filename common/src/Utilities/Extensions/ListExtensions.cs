using System.Collections.Generic;
using System.Linq;

namespace Blogposts.Common.Utilities.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Checks if the list has unique values
        /// </summary>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <param name="list">The list</param>
        /// <returns>True if all the values are unique otherwise false</returns>
        public static bool HasUniqueValues<T>(this List<T> list)
        {
            bool hasUniqueValues = list.Distinct().Count() == list.Count();

            return hasUniqueValues;
        }

        /// <summary>
        /// Returns an empty list when the original list is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static List<T> EmptyIfNull<T>(this List<T> list)
        {
            return list ?? new List<T>();
        }
    }
}