using Microsoft.Extensions.Configuration;
using System.IO;

namespace Blogposts.Common.Configuration.Providers.Configuration
{
    /// <summary>
    /// Interface provide access to remote or test configuration.
    /// </summary>
    public interface IConfigurationQueries
    {
        /// <summary>
        /// interface to get configuration Blob like stream.
        /// </summary>
        /// <param name="configuration">local configuration</param>
        /// <param name="envName">Dev, Test, Staging or Production</param>
        /// <param name="serviceName">typeof(Program).Namespace</param>
        /// <returns>configuration text stream</returns>
        Stream GetConfiguration(IConfiguration configuration, string envName, string serviceName);
    }
}
