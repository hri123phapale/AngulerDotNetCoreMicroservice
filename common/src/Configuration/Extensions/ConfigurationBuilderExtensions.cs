using Azure.Identity;
using Blogposts.Common.Configuration.Providers.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System.IO;
using System.Runtime.InteropServices;

namespace Blogposts.Common.Configuration.Extensions
{
    /// <summary>
    /// Extension class for ConfigurationBuilder to support custom configuration features
    /// </summary>
    public static class ConfigurationBuilderExtensions
    {
        /// <summary>
        /// Sets up the configuration system using either a remote endpoint or local config files.
        /// </summary>
        /// <param name="configurationBuilder">current ConfigurationBuilder</param>
        /// <param name="envName">The environment for which we are configuring the current process.</param>
        /// <param name="serviceName">The key used to identify the configuration to fetch.</param>
        /// <returns>An <see cref="IConfigurationBuilder"/> enriched with configruation from the resolved source.</returns>
        public static IConfigurationBuilder AddConfiguration(this IConfigurationBuilder configurationBuilder, string envName, string serviceName)
        {
            var serviceConfigurationProvider = new ServiceConfigurationProvider(envName, serviceName);
            serviceConfigurationProvider.GetConfiguration(configurationBuilder);
            return configurationBuilder;
        }

        /// <summary>
        /// If the connection string is not empty, it adds the Azure app configuration.
        /// It configures also the key vault which holds the secrests and retrieves the keys for the given labels
        /// </summary>
        /// <param name="configurationBuilder">The instance of the configuration builder</param>
        /// <param name="azureAppConnString">The connection string of the azure app configuration</param>
        /// <param name="labels">Indicate the service that each key belongs</param>
        /// <returns>An <see cref="IConfigurationBuilder"/> enriched with configruation from the resolved source.</returns>
        public static IConfigurationBuilder AddAzureAppConfig(this IConfigurationBuilder configurationBuilder, string azureAppConnString, params string[] labels)
        {
            if (string.IsNullOrEmpty(azureAppConnString))
            {
                return configurationBuilder;
            }

            configurationBuilder.AddAzureAppConfiguration(options =>
            {
                options.Connect(azureAppConnString)
                    .ConfigureKeyVault(kv =>
                    {
                        kv.SetCredential(new DefaultAzureCredential());
                    });

                options.Select(KeyFilter.Any, "common-settings");
                foreach (string label in labels)
                {
                    options.Select(KeyFilter.Any, label);
                }
            });

            return configurationBuilder;
        }

        /// <summary>
        /// Adds the appsetting files
        /// </summary>
        /// <param name="envName">The environment name</param>
        /// <returns>The <see cref="IConfigurationBuilder"/> instance</returns>
        public static IConfigurationBuilder GetConfiguration(string envName)
        {
            return GetConfiguration(envName, null);
        }

        /// <summary>
        /// Adds the appsetting files
        /// </summary>
        /// <param name="envName">The environment name</param>
        /// <param name="configurationBuilder">The configuration builder</param>
        /// <param name="labels">The configuration labels that are going to be retrieved from the app configuration</param>
        /// <returns>The <see cref="IConfigurationBuilder"/> instance</returns>
        public static IConfigurationBuilder GetConfiguration(string envName, IConfigurationBuilder configurationBuilder, params string[] labels)
        {
            configurationBuilder ??= new ConfigurationBuilder();

            configurationBuilder = configurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

#if DEBUG
            configurationBuilder = configurationBuilder.AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                configurationBuilder = configurationBuilder.AddJsonFile($"appsettings.{envName}.Linux.json", optional: true, reloadOnChange: true);
            }
#endif
            configurationBuilder = configurationBuilder.AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();
            if (!string.IsNullOrEmpty(configuration["AppConfiguration:ConnectionString"]))
            {
                configurationBuilder.AddAzureAppConfig(configuration["AppConfiguration:ConnectionString"], labels);
            }

            return configurationBuilder;
        }
    }
}