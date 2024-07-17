using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Blogposts.Common.Configuration.Providers.Configuration
{
    /// <summary>
    /// A backend/frontend process to call a remote Configuration provider endpoint at startup
    /// receives back a JSON blob that represents the requested configuration.
    /// The received configuration is added to Dependency Injection(i.e.IConfiguration).
    /// The service updates the previously stored configuration in DI with the newly received configuration data.
    /// The configuration data inside the appsettings.json file of the process only contains the URL of the ApiGateway (UseServiceConfiguration key),  
    /// behind which is the Configuration service endpoint
    /// </summary>
    public class ServiceConfigurationProvider : IServiceConfigurationProvider
    {
        private readonly string _envName;
        private readonly string _serviceName;
        private readonly IConfigurationQueries _configurationQueries;

        /// <summary>
        /// Call of ConfigurationQueries to remote configuration or get existing IConfigurationQueries implementation
        /// </summary>
        /// <param name="envName"></param>
        /// <param name="serviceName"></param>
        /// <param name="args"></param>
        /// <param name="configurationQueries"></param>
        public ServiceConfigurationProvider(string envName, string serviceName, IConfigurationQueries configurationQueries = null)
        {
            _envName = envName;
            _serviceName = serviceName;
            _configurationQueries = configurationQueries ?? new ConfigurationQueries();
        }

        /// <inheritdoc/>
        /// <remarks>Calls a remote endpoint to get configuration data. 
        /// If remote configuration does not exist, configuration is loaded from local settings files.</remarks>
        public void GetConfiguration(IConfigurationBuilder configurationBuilder)
        {
            var configurationLocal = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_envName}.json", optional: true, reloadOnChange: true)
                .Build();

            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());

            Stream jsonstream = null;
           
            if (configurationLocal["Runtime:UseServiceConfiguration"] == "True")
            {
                jsonstream = _configurationQueries.GetConfiguration(configurationLocal, _envName, _serviceName);
            }

            if (jsonstream != null)
            {
                configurationBuilder
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonStream(jsonstream)
                    .AddEnvironmentVariables();
            }
            else
            {
                configurationBuilder
                     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{_envName}.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();
            }
        }
    }
}