using System;
using System.Reflection;

namespace Blogposts.Common.Utilities
{
    public static class ReflectionUtilities
    {
        /// <summary>
        /// Get's the property value
        /// </summary>
        /// <param name="obj">The obj</param>
        /// <param name="propertyName">The property name</param>
        /// <returns>The property value</returns>
        public static object GetPropertyValue(object obj, string propertyName)
        {
            Type objType = obj.GetType();
            PropertyInfo propInfo = GetPropertyInfo(objType, propertyName);

            return propInfo.GetValue(obj, null);
        }

        /// <summary>
        /// Get's the property info
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="propertyName">The property name</param>
        /// <returns>The property info</returns>
        public static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo propInfo;
            do
            {
                propInfo = type.GetProperty(propertyName,
                       BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                type = type.BaseType;
            }
            while (propInfo == null && type != null);

            return propInfo;
        }
    }
}