using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceStack;
using System;
using System.IO;
using System.Text;

namespace Blogposts.Common.Configuration.Providers.Configuration
{
    /// <summary>
    /// Implementation of providing of remote configuration,
    /// additional configuration for local configuration.
    /// If remote configuration is not exists or is not able, then returns null value.
    /// </summary>
    public class ConfigurationQueries : IConfigurationQueries
    {
        /// <summary>
        /// Get the remote configuration and convert it into stream
        /// </summary>
        /// <param name="configuration">local configuration with UseServiceConfiguration key</param>
        /// <param name="envName">environment </param>
        /// <param name="serviceName">namespace </param>
        /// <returns>JSON blob stream that represents the requested configuration</returns>
        public Stream GetConfiguration(IConfiguration configuration, string envName, string serviceName)
        {
            try
            {
                var client = new JsonServiceClient(configuration["Api:ApiGatewayUrl"]);
                string response = client.Get<string>(new QueryConfiguration { Environment = envName, Namespace = serviceName });
                string configJson = JsonConvert.DeserializeObject(response).ToString().Convert();
                byte[] byteArray = Encoding.ASCII.GetBytes(configJson);

                return new MemoryStream(byteArray);
            }
            catch (Exception ex)
            {
                throw new ConfigurationNotFoundException("There was a problem while getting remote configuration", ex);
            }
        }
    }
}