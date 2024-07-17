using Blogposts.Common.Logging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogposts.Common.Logging.Extensions
{
    public static class ServiceCollectionExtensions
    {
        #region Public methods

        /// <summary>
        /// Adds Log
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration</param>
        /// <returns></returns>
        public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
        {
            LogConfiguration.Configure(configuration);

            services.AddScoped<IStartTimerService, StartTimerService>();

            return services;
        }

        #endregion
    }
}