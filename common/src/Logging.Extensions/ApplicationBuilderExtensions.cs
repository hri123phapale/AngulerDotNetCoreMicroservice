using Blogposts.Common.Logging.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Blogposts.Common.Logging.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds middleware for logging requests
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<HttpSerilogMiddleware>();

            return app;
        }

        /// <summary>
        /// Adds middleware for logging requests
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHttpStartTimer(this IApplicationBuilder app)
        {
            app.UseMiddleware<HttpSerilogStartTimerMiddleware>();

            return app;
        }
    }
}