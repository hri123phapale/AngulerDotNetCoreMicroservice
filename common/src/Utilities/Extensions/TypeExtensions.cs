using System;
using System.Reflection;

namespace Blogposts.Common.Utilities.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks whether a type implements a specific interface
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="type"></param>
        /// <returns>true if it does implement the interface else false</returns>
        public static bool Implements<TInterface>(this Type type) where TInterface : class
        {
            return typeof(TInterface).IsAssignableFrom(type);
        }

        /// <summary>
        /// This will try and get the value of a property from an object that the type is not known
        /// </summary>
        /// <typeparam name="T">The type of the property to be returned</typeparam>
        /// <param name="obj">The object to check if it has a property of propertyName and try retrieve the value</param>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="value">the value of the property</param>
        /// <returns>Will return true if the property exists and retrieved successfully</returns>
        public static bool TryGetValue<T>(this object obj, string propertyName, out T value)
        {
            value = default(T);
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property != null)
            {
                value = (T)property.GetValue(obj);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Extension method to check if a type is a primitive
        /// </summary>
        public static bool IsPrimitive(this Type type)
        {
            return (type.IsPrimitive || type == typeof(decimal) || type == typeof(string));
        }
    }
}
