using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Blogposts.Common.Logging
{
    /// <summary>
    /// Provides extension methods to configure logging infrastructure.
    /// </summary>
    /// <remarks>The logging infrastructure is built on Serilog. Serilog configuration settings
    /// must be present in the appsettings.json file in order to apply a file-based configuration.
    /// If no Serilog settings exist in the appsettings.json file, hardcoded defaults will be applied.</remarks>
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// Enable logging using Blogposts logging infrastructure.
        /// </summary>
        /// <param name="webHostBuilder">The <see cref="IWebHostBuilder"/> instance on which to activate logging.</param>
        /// <param name="config">The <see cref="IConfigurationRoot"/> instance which provides the Serilog settings from the appsettings.json file.</param>
        /// <returns>An instance of </returns>
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder, IConfigurationRoot config)
        {
            LogConfiguration.Configure(config);

            webHostBuilder.UseSerilog();

            return webHostBuilder;
        }
    }
}