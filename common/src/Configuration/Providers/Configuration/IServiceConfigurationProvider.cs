using Microsoft.Extensions.Configuration;

namespace Blogposts.Common.Configuration.Providers.Configuration
{
    /// <summary>
    /// Interface provide end point to configuration
    /// </summary>
    public interface IServiceConfigurationProvider
    {
        /// <summary>
        /// Sets up the sources of the configuration builder.
        /// </summary>
        /// <returns></returns>
        void GetConfiguration(IConfigurationBuilder configurationBuilder);
    }
}