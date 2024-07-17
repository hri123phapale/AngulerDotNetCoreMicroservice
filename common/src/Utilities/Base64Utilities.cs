using System;
using System.Text;

namespace Blogposts.Common.Utilities
{
    /// <summary>
    /// Utility class for encoding and decoding in Base64
    /// </summary>
    public static class Base64Utilities
    {
        /// <summary>
        /// Gets an encoded string and decodes it
        /// </summary>
        /// <param name="value">The encoded string</param>
        /// <returns>The decoded string</returns>
        public static string Decode(string value)
        {
            byte[] valueBytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }

        /// <summary>
        /// Gets a string and encodes it to Base64
        /// </summary>
        /// <param name="value">The string</param>
        /// <returns>The encoded string</returns>
        public static string Encode(string value)
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }
    }
}