using System.Security.Cryptography;
using System.Text;

namespace Blogposts.Common.Utilities
{
    /// <summary>
    /// Helper class to generate random strings
    /// </summary>
    public class RandomStringGenerator
    {
        /// <summary>
        /// Generates a random alphanumeric code
        /// </summary>
        /// <param name="size">the size of the string</param>
        /// <param name="uppercase">whether the result should be in uppercase</param>
        /// <returns>the random string</returns>
        public static string GetRandomAlphanumericCode(int size, bool uppercase = false)
        {
            string validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] validCharactersArray = validCharacters.ToCharArray();

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

            byte[] data = new byte[size];
            crypto.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(size);

            foreach (byte b in data)
            {
                result.Append(validCharactersArray[b % validCharactersArray.Length]);
            }

            string code = result.ToString();

            if (uppercase)
            {
                code = code.ToUpper();
            }

            return code;
        }
    }
}