using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Blogposts.Common.Utilities.Reflection
{
    /// <summary>
    /// Register types
    /// </summary>
    public static class TypeRegisterer
    {
        /// <summary>
        /// Register query handlers
        /// </summary>
        /// <param name="assemblies">List of assemblies</param>
        /// <param name="services">Reference to an instance of IServiceCollection</param>
        public static void RegisterQueryHandler(Assembly[] assemblies, IServiceCollection services)
        {
            foreach (var assembly in assemblies)
            {
                var type = assembly.GetTypes().FirstOrDefault(x => x.FullName != null &&
                                                                   x.FullName.Contains("ServiceCollectionExtensions"));
                if (type != null)
                {
                    var method = type.GetMethod("AddQueryHandlers", BindingFlags.Public | BindingFlags.Static);
                    if (method != null)
                    {
                        method.Invoke(null, new object[] { services });
                    }
                }
            }
        }
    }
}