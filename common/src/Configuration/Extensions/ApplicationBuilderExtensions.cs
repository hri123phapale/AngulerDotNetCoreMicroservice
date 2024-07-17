using Microsoft.AspNetCore.Builder;

namespace Blogposts.Common.Configuration.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Extention Method which registers the AppConfiguration Service.
        /// Enables automatic configuration refresh from Azure App Configuration.
        /// </summary>
        /// <param name="app">The application builder instance</param>
        /// <returns>The enhanced application builder instance</returns>
        public static IApplicationBuilder UseAppConfiguration(this IApplicationBuilder app)
        {
            app.UseAzureAppConfiguration();

            return app;
        }
    }
}