using System.Globalization;

namespace Blogposts.Common.Utilities
{
    public static class CultureValidator
    {
        /// <summary>
        /// Creates a culture info object with the given culture code to validate if it is valid
        /// </summary>
        /// <param name="cultureCode">The culture code</param>
        /// <returns>True if is valid, False if given code is not a valid culture code.</returns>
        public static bool IsValid(string cultureCode)
        {
            if (string.IsNullOrWhiteSpace(cultureCode))
            {
                return false;
            }

            try
            {
                if (!string.IsNullOrEmpty(cultureCode))
                {
                    var culture = new CultureInfo(cultureCode);
                    _ = CultureInfo.GetCultureInfo(culture.LCID);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}