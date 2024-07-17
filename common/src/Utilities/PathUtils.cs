using System.IO;
using System.Text.RegularExpressions;

namespace Blogposts.Common.Utilities
{
    /// <summary>
    /// Class containing URL related utility methods
    /// </summary>
    public class PathUtils
    {
        public static string UrlCombine(params string[] components)
        {
            bool isUnixPath = Path.DirectorySeparatorChar == '/';

            for (var i = 1; i < components.Length; i++)
            {
                if (Path.IsPathRooted(components[i]))
                {
                    components[i] = components[i].TrimStart('/', '\\');
                }
            }

            string url = Path.Combine(components);

            if (!isUnixPath)
            {
                url = url.Replace(Path.DirectorySeparatorChar, '/');
            }

            return Regex.Replace(url, @"(?<!(http:|https:))//", @"/");
        }

        /// <summary>
        /// Returns the file name and extension of the specified <paramref name="path"/> string.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// The characters after the last directory separator character in <paramref name="path"/>. If the last character of <paramref name="path"/> is a directory or volume separator character, this method returns <see cref="string.Empty"/>.
        /// If <paramref name="path"/> is <c>null</c>, this method returns <c>null</c>.
        /// </returns>
        public static string GetFileName(string path)
        {
            path = path?.Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);

            return Path.GetFileName(path);
        }
    }
}