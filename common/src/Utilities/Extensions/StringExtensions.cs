using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Blogposts.Common.Utilities.Extensions
{
    /// <summary>
    /// Extension methods for strings
    /// </summary>
    public static class StringExtensions
    {
        private static Regex EmailRegex = new Regex("^[^_]*_");
        private const int MaxLength = 5000;

        /// <summary>
        /// Returns the length of a string, and returns 0 if the string is null or empty
        /// </summary>
        public static int NotNullLength(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }

            return value.Length;
        }

        /// <summary>
        /// Decodes a url encoded string
        /// </summary>
        /// <param name="source">The encoded url string</param>
        /// <returns>The decoded string</returns>
        public static string Decoded(this string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                return HttpUtility.UrlDecode(source);
            }
            return source;
        }

        /// <summary>
        /// Checks if the given string is base 64 encoded string
        /// </summary>
        /// <param name="input">The string to be checked</param>
        /// <returns>True if the string is base 64</returns>
        public static bool IsBase64String(this string input)
        {
            var buffer = new Span<byte>(new byte[input.Length]);
            return Convert.TryFromBase64String(input, buffer, out int bytesParsed);
        }

        /// <summary>
        /// This will convert the first char to lower case
        /// </summary>
        public static string ToLowerFirstChar(this string source)
        {
            string newString = source;
            if (!string.IsNullOrEmpty(newString) && char.IsUpper(newString[0]))
            {
                newString = char.ToLower(newString[0]) + newString.Substring(1);
            }

            return newString;
        }

        /// <summary>
        /// Convert string to came case
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>The camel case string</returns>
        public static string ToCamelCase(this string str)
        {
            return !string.IsNullOrEmpty(str) && str.Length > 1 ? char.ToLowerInvariant(str[0]) + str.Substring(1) : str;
        }

        /// <summary>
        /// This will remove the azureb2c id appended to the email address from active directory
        /// </summary>
        public static string RemoveB2Ccode(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return EmailRegex.Replace(str, string.Empty);
            }
            return str;
        }

        /// <summary>
        /// This will remove invalid path & file characters from the given string
        /// </summary>
        public static string ReplaceInvalidFileCharacters(this string input, string replacement)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()) + new string(GetInvalidFileNameChars());

            foreach (char c in invalid)
            {
                input = input.Replace(c.ToString(), replacement);
            }

            return input;
        }

        public static char[] GetInvalidFileNameChars() => new char[]
        {
            '\"', '<', '>', '|', '\0', '"',
            (char)1, (char)2, (char)3, (char)4, (char)5, (char)6, (char)7, (char)8, (char)9, (char)10,
            (char)11, (char)12, (char)13, (char)14, (char)15, (char)16, (char)17, (char)18, (char)19, (char)20,
            (char)21, (char)22, (char)23, (char)24, (char)25, (char)26, (char)27, (char)28, (char)29, (char)30,
            (char)31, ':', '*', '?', '\\', '/'
        };

        public static char[] GetInvalidPathChars() => new char[]
        {
            '|', '\0',
            (char)1, (char)2, (char)3, (char)4, (char)5, (char)6, (char)7, (char)8, (char)9, (char)10,
            (char)11, (char)12, (char)13, (char)14, (char)15, (char)16, (char)17, (char)18, (char)19, (char)20,
            (char)21, (char)22, (char)23, (char)24, (char)25, (char)26, (char)27, (char)28, (char)29, (char)30,
            (char)31
        };

        /// <summary>
        /// Removes all whitespace from a string
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns>Returns the string without whitespace</returns>
        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }

        /// <summary>
        /// Replaces all line ending characters to html line breaks
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns>Returns the string after replacement</returns>
        public static string ReplaceLineEndings(this string input, string replacement)
        {
            if (input is null)
            {
                return null;
            }

            var expression = new Regex(@"(\r\n|\n|\r)");

            return expression.Replace(input, replacement);
        }

        /// <summary>
        /// Returns the given value in a list
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns>Returns the list</returns>
        public static IEnumerable<string> ConvertToList(this string input)
        {
            if (input is null)
            {
                return null;
            }

            return new List<string> { input };
        }

        /// <summary>
        /// Converts the string to the required enum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this string val)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), val);
        }

        /// <summary>
        /// This will convert a byte count to string with relevant suffix
        /// </summary>
        public static string BytesToString(this long byteCount)
        {
            string[] suf = { " B", " KB", " MB", " GB", " TB", " PB", " EB" }; //Longs run out around EB
            if (byteCount == 0)
            {
                return "0" + suf[0];
            }

            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        /// <summary>
        /// Retrieve the string by max length
        /// </summary>
        /// <param name="val">The string</param>
        /// <param name="maxLength">The max length of the string</param>
        /// <returns></returns>
        public static string TrimByMaxLenth(this string val, int maxLength = MaxLength)
        {
            return val.Length > maxLength ? val.Substring(0, maxLength - 1) + "..." : val;
        }

        /// <summary>
        /// Removes leading and trailing white-space characters and returns null if the string that remains is empty.
        /// </summary>
        /// <param name="val">The string.</param>
        /// <returns></returns>
        public static string TrimToNull(this string val)
        {
            val = val?.Trim();
            return string.IsNullOrEmpty(val) ? null : val;
        }

        /// <summary>
        /// Replaces the specified argument in the source string with the given value.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="argument">The argument to replace.</param>
        /// <param name="value">The value to replace the argument with.</param>
        /// <returns>The modified string with the argument replaced.</returns>
        public static string ReplaceArgument(this string source, string argument, string value)
        {
            return source.ReplaceArguments(new Dictionary<string, string> { { argument, value } });
        }

        /// <summary>
        /// Replaces the specified argument in the source string with the given value.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="arguments">The dictionary of arguments to replace.</param>
        /// <returns>The modified string with the arguments replaced.</returns>
        public static string ReplaceArguments(this string source, Dictionary<string, string> arguments)
        {
            if (string.IsNullOrEmpty(source) || arguments.EmptyIfNull().NotAny())
            {
                return source;
            }

            foreach (var argument in arguments)
            {
                source = source.Replace($"{{{argument.Key}}}", argument.Value);
            }

            return source;
        }
    }
}