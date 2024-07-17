using Blogposts.Common.Utilities.Abstractions;
using Blogposts.Common.Utilities.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Blogposts.Common.Utilities.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IOptionsMonitor<T> GetOptions<T>(this IServiceCollection services)
        {
            return services
                .BuildServiceProvider()
                .GetService<IOptionsMonitor<T>>();
        }

        public static IServiceCollection AddCdnOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure<CdnOptions>(x => configuration.GetSection("Cdn").Bind(x));
        }

        public static IServiceCollection AddSynchronizationBarrier(this IServiceCollection services)
        {
            return services.AddScoped<ISynchronizationBarrier, SynchronizationBarrier>();
        }
    }
}