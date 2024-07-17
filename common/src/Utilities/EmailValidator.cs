using System.Globalization;
using System.Text.RegularExpressions;

namespace Blogposts.Common.Utilities
{
    public static class EmailValidator
    {
        public const string EmailValidatorRegex =
                 @"^[^\W_]*[a-zA-Z0-9äöüÄÖÜß.+_-]+(?:\.[a-zA-Z0-9äöüÄÖÜß.+_-]+)*(?<!\+)[_-]?@(?:[a-zA-Z0-9äöüÄÖÜß](?:[a-zA-Z0-9äöüÄÖÜß-]*[a-zA-Z0-9äöüÄÖÜß])?\.)+[a-zA-Z0-9äöüÄÖÜß](?:[a-zA-Z0-9äöüÄÖÜß-]*[a-zA-Z0-9äöüÄÖÜß])?$";

        public static bool IsValid(string information)
        {
            if (string.IsNullOrWhiteSpace(information))
            {
                return false;
            }

            try
            {
                // Normalize the domain
                information = Regex.Replace(information, @"(@)(.+)$", DomainMapper, RegexOptions.None);

                return Regex.IsMatch(information, EmailValidatorRegex, RegexOptions.IgnoreCase);

                // Examines the domain part of the email and normalizes it.
                static string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}