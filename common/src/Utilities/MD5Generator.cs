using System.Security.Cryptography;
using System.Text;

namespace Blogposts.Common.Utilities
{
    /// <summary>
    /// Class that generates an MD5 hash.
    /// </summary>
    public static class MD5Generator
    {
        /// <summary>
        /// Generates an MD5 hash from a string
        /// </summary>
        /// <param name="input">The string to be hashed</param>
        /// <returns>The MD5 hashed string</returns>
        public static string Create(string input)
        {
            var hash = new StringBuilder();
            var md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}