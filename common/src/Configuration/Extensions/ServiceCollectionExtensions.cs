using Blogposts.Common.Configuration.Elasticsearch;
using Blogposts.Common.Configuration.Options;
using Blogposts.Common.Configuration.Options.Elasticsearch;
using Blogposts.Common.Configuration.Providers.Runtime;
using Blogposts.Common.Configuration.Providers.Runtime.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blogposts.Common.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddElasticClientProvider(this IServiceCollection services, Action<ElasticClientProviderOptions> setupAction)
        {
            services.Configure<ElasticClientProviderOptions>(setupAction);

            services.AddSingleton<IElasticClientProvider, DefaultElasticClientProvider>();

            return services;
        }

        /// <summary>
        /// Adds IInstanceIdProvider as a singleton
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <returns>Service Collection</returns>
        public static IServiceCollection AddInstanceIdProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRuntimeOptions(configuration)
                .AddSingleton<IInstanceIdProvider, ServiceFabricIdProvider>();
            return services;
        }

        /// <summary>
        /// Extension method which registers the strongly typed runtime options
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> instance</param>
        /// <param name="configuration">The configuration instance containing the settings</param>
        /// <returns>The enriched <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddRuntimeOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure<RuntimeOptions>(x => configuration.GetSection("Runtime").Bind(x));
        }

        public static IServiceCollection AddScriptOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure<ScriptOptions>(x => configuration.GetSection("Script").Bind(x));
        }



        /// <summary>
        /// Extention Method which registers the AppConfiguration Service and the the strongly typed azure app configuration options
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure<AppConfigurationOptions>(x => configuration.GetSection("AppConfiguration").Bind(x))
                           .AddAzureAppConfiguration();
            
        }
    }
}