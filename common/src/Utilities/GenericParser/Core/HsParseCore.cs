namespace Blogposts.Common.Utilities.GenericParser.Core
{
    /// <summary>
    /// The base for parsers, all the parsers will implement this core class
    /// and will allow certain functions to be performed on the generic types
    /// Our components should not be aware of the generic types they contain
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class HsParseCore<T>
    {
        /// <summary>
        /// Allows the formating of the generic type to a string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public abstract string FormatValueAsString(T value, string format);

        /// <summary>
        /// This will parse a string value to the generic type
        /// </summary>
        public abstract T ParseFromString(string value, string format);

        /// <summary>
        /// Increase the generic value by a step, clamping to max
        /// </summary>
        public abstract T Increase(T value, T step, T max);

        /// <summary>
        /// Decrease the generic value by a step, clamping to a minimum
        /// </summary>
        public abstract T Decrease(T value, T step, T min);

        /// <summary>
        /// Round a generic value, to accuracy of decimal place
        /// </summary>
        public abstract T Round(T value, int decPlace);

        /// <summary>
        /// Gets the minimum
        /// </summary>
        public abstract T GetMinimum();

        /// <summary>
        /// Gets the maximum
        /// </summary>
        public abstract T GetMaximum();

        /// <summary>
        /// Gets the step
        /// </summary>
        public abstract T GetStep();

        /// <summary>
        /// Parses a generic from bool, can return null if parsing fails
        /// </summary>
        public abstract T FromBoolNull(bool? value, bool indeterminate);

        /// <summary>
        /// Parses to a generic type from decimal value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract T FromDecimal(decimal value);

        /// <summary>
        /// Parses a generic value to a bool
        /// </summary>
        public abstract bool ToBool(T value);

        /// <summary>
        /// Parses a bool to a generic value
        /// </summary>
        public abstract T FromBool(bool value);

        /// <summary>
        /// Will return a parser based on the generic value
        /// </summary>
        public static HsParseCore<T> Get()
        {
            return HsCoreParse.Parsers.Get<HsParseCore<T>>();
        }
    }
}