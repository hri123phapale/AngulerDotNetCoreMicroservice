using Blogposts.Common.Utilities.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Blogposts.Common.Utilities.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Prime number used for hash creation
        /// </summary>
        private const int Prime1 = 149;

        /// <summary>
        /// Prime number used for hash creation
        /// </summary>
        private const int Prime2 = 397;

        /// <summary>
        /// Converts an object to Dictionary of string key value pairs
        /// </summary>
        public static Dictionary<string, string> ToDictionary(this object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        /// <summary>
        /// Generates a hash for the object based on property values
        /// </summary>
        public static ulong GenerateHash(this object obj)
        {
            ulong hash = 0;
            if (obj == null || obj.ToString() == string.Empty)
            {
                return hash;
            }

            Type objType = obj.GetType();

            if (objType.IsValueType || obj is string)
            {
                unchecked
                {

                    if (obj is Guid)
                    {
                        foreach (byte b in ((Guid)obj).ToByteArray())
                        {
                            hash ^= hash * Prime2 + b;
                        }
                    }
                    hash ^= (uint)obj.GetHashCode() * Prime2;
                }

                return hash;
            }

            if (typeof(IEnumerable).IsAssignableFrom(objType))
            {
                IEnumerable enumer = (IEnumerable)obj;
                unchecked
                {
                    foreach (object o in enumer)
                    {
                        hash ^= o.GenerateHash();
                    }
                }
            }
            else
            {
                unchecked
                {
                    foreach (PropertyInfo property in obj.GetType().GetProperties())
                    {
                        if (property.GetCustomAttribute<ExcludeFromHashAttribute>() == null)
                        {
                            object value = property.GetValue(obj, null);
                            hash ^= value.GenerateHash();
                        }
                    }
                }
            }

            return hash;
        }

        /// <summary>
        /// Used to cast an object to a specified type
        /// </summary>
        public static T CastTo<T>(this object obj) where T : class
        {
            return (T)obj;
        }
    }
}