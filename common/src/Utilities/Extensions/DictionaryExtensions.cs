using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Blogposts.Common.Utilities.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// This will check if two dictionaries containing <see cref="IEnumerable{String}"/> as values are equal checking keys match and values match
        /// </summary>
        /// <param name="source">Source Dictionary</param>
        /// <param name="target">Target Dictionary</param>
        /// <returns>true if the dictionary keys and values match</returns>
        public static bool AreEqual(this Dictionary<string, IEnumerable<string>> source, Dictionary<string, IEnumerable<string>> target)
        {
            return source.Keys.Count == target.Keys.Count && target.Keys.All(key => source.ContainsKey(key) && source[key].SequenceEqual(target[key]));
        }

        /// <summary>
        /// This will merge two of the same dictionaries into one, this will overwrite the values in the source
        /// dictionary with the values from the target dictionary if they same key exists in both
        /// </summary>
        /// <typeparam name="TKey">Key type of the dictionary</typeparam>
        /// <typeparam name="TValue">Value type of the dictionary</typeparam>
        /// <param name="source">First dictionary</param>
        /// <param name="target">Second dictionary</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> source, Dictionary<TKey, TValue> target)
        {
            foreach (KeyValuePair<TKey, TValue> item in target)
            {
                source[item.Key] = item.Value;
            }
            return source;
        }

        /// <summary>
        /// Merges a <see cref="NameValueCollection"/> into the given source dictionary, this will overwrite any keys that already exist
        /// </summary>
        public static Dictionary<string, string> Merge(this Dictionary<string, string> source, NameValueCollection target)
        {
            foreach (string key in target)
            {
                source[key] = target[key];
            }
            return source;
        }

        /// <summary>
        /// This will try get a value from a dictionary of objects and cast that object to a requested type
        /// </summary>
        /// <typeparam name="TReturnValue">The type to cast the object to</typeparam>
        public static TReturnValue GetValue<TReturnValue>(this IDictionary<string, object> dictionary, string key)
        {
            TReturnValue returnValue = default(TReturnValue);
            if (dictionary == null)
            {
                return returnValue;
            }

            if (dictionary.TryGetValue(key, out object result))
            {
                return (TReturnValue)result;
            }

            return returnValue;
        }

        /// <summary>
        /// Helper function to convert key value pair to Dictionary
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            return source.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);
        }

        /// <summary>
        /// Adds a range of values contained in another dictionary to the dictionary
        /// </summary>
        public static void AddRange<T, S>(this Dictionary<T, S> source, Dictionary<T, S> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection), "Empty collection");
            }

            foreach (var item in collection)
            {
                if (!source.ContainsKey(item.Key))
                {
                    source.Add(item.Key, item.Value);
                }
            }
        }

        /// <summary>
        /// Tries to add an item, if it already exists we overwrite that value in the dictionary
        /// </summary>
        /// <typeparam name="TKey">The type of key</typeparam>
        /// <typeparam name="TValue">The type of value</typeparam>
        /// <param name="source">The given dictionary</param>
        /// <param name="key">the key to set</param>
        /// <param name="value">the value to set</param>
        public static void AddOrSet<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, TValue value)
        {
            if (value == null)
                throw new ArgumentException("key");

            if (source.ContainsKey(key))
            {
                source[key] = value;
            }
            else
            {
                source.Add(key, value);
            }
        }

        /// <summary>
        /// Tries to get a value from a dictionary
        /// </summary>
        /// <typeparam name="TKey">The type of key</typeparam>
        /// <typeparam name="TValue">The type of the value</typeparam>
        /// <param name="source">The dictionary to retrieve the value from</param>
        /// <param name="key">The key to retrieve</param>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key)
        {
            TValue val;
            source.TryGetValue(key, out val);
            return val;
        }
    }
}