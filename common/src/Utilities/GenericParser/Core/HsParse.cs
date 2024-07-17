using Blogposts.Common.Utilities.GenericParser.Parsers;
using System;
using System.Collections.Generic;

namespace Blogposts.Common.Utilities.GenericParser.Core
{
    /// <summary>
    /// HsParse will maintain a dictionary of generic HsParseCore<T> that will allow certain
    /// functions to be run and parsed on generic values
    /// </summary>
    public class HsParse
    {
        private readonly Dictionary<Type, object> _dictionary = new Dictionary<Type, object>();

        /// <summary>
        /// This will return a parse instance based on the type
        /// i.e. we can parse generic values to strings, bools, ints, numbers etc
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="action">Instance of the Parser</param>
        public HsParse Case<T>(T action)
        {
            _dictionary[typeof(T)] = action;
            return this;
        }

        /// <summary>
        /// Gets the parser based on the generic type
        /// </summary>
        /// <typeparam name="T">Type to get a parser for</typeparam>
        /// <returns>instance of the parser</returns>
        public T Get<T>()
        {
            if (_dictionary.TryGetValue(typeof(T), out object result))
            {
                return (T)result;
            }

            return (T)(object)null;
        }
    }

    public class HsCoreParse

    {
        protected HsCoreParse()
        {
        }

        /// <summary>
        /// Keeps a list of parsers using the generic type as a key
        /// </summary>
        public static readonly HsParse Parsers = new HsParse()
            .Case<HsParseCore<string>>(new HsParseString())
            .Case<HsParseCore<Guid?>>(new HsParseGuidNull())
            .Case<HsParseCore<int?>>(new HsParseIntNull())
            .Case<HsParseCore<int>>(new HsParseInt())
            .Case<HsParseCore<DateTime>>(new HsParseDateTime())
            .Case<HsParseCore<short?>>(new HsParseShortNull());
    }
}