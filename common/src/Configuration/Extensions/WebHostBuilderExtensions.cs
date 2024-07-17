using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Blogposts.Common.Configuration.Extensions
{
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// Extension method for <see cref="IWebHostBuilder"/>.
        /// It retrieves the appsettings configuration files and adds Azure app configuration if applicable
        /// </summary>
        /// <param name="webHostBuilder">The <see cref="IWebHostBuilder"/> instance on which to activate logging.</param>
        /// <param name="configuration">The configuration instance</param>
        /// <param name="envName">The environment name</param>
        /// <param name="labels">The configuration labels that are going to be retrieved from the app configuration</param>
        /// <returns>The <see cref="IWebHostBuilder"/> instance </returns>
        public static IWebHostBuilder ConfigureAzureAppConfig(this IWebHostBuilder webHostBuilder, IConfigurationRoot configuration, string envName, params string[] labels)
        {
            webHostBuilder.ConfigureAppConfiguration(builder =>
            {
                builder.Build();
                ConfigurationBuilderExtensions.GetConfiguration(envName, builder);
                builder.AddAzureAppConfig(configuration["AppConfiguration:ConnectionString"], labels);
            });

            return webHostBuilder;
        }
    }
}