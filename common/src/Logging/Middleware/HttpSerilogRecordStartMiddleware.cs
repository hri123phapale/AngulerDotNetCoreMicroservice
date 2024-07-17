using Blogposts.Common.Logging.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Blogposts.Common.Logging.Middleware
{
    public class HttpSerilogStartTimerMiddleware
    {
        #region Private Members

        private readonly RequestDelegate _next;

        #endregion

        #region Constructor

        public HttpSerilogStartTimerMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        #endregion

        #region Public Methods

        public Task Invoke(HttpContext httpContext, IStartTimerService startTimerService)
        {
            startTimerService.StartTimer();

            return _next(httpContext);
        }

        #endregion
    }
}