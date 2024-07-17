using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using static System.IO.Directory;

namespace Blogposts.Common.Utilities.Reflection
{
    /// <summary>
    /// Get the list of assemblies
    /// </summary>
    public static class AssemblyFinder
    {
        /// <summary>
        /// Get the list of assemblies
        /// </summary>
        /// <param name="includePatterns">The list of include patterns</param>
        /// <param name="excludePatterns">The list of exclude patterns</param>
        /// <returns>The list of assemblies</returns>
        public static List<Assembly> GetAssembly(string[] includePatterns, string[] excludePatterns)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var files = GetFiles(path)
                .Where(file => includePatterns.Any(pattern => Regex.IsMatch(file, pattern)) &&
                               !excludePatterns.Any(pattern => Regex.IsMatch(file, pattern)));

            var assemblyList = new List<Assembly>();

            foreach (string file in files)
            {
                assemblyList.Add(Assembly.LoadFrom(file));
            }

            return assemblyList.ToList();
        }
    }
}