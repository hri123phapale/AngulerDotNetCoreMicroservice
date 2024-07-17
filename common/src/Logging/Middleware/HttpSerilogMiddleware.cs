using Blogposts.Common.Logging.Services;
using Blogposts.Common.Security.Abstractions.Identity;
using Blogposts.Common.Utilities;
using Blogposts.Common.Utilities.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Primitives;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogposts.Common.Logging.Middleware
{
    public class HttpSerilogMiddleware
    {
        #region Private Members

        private const string MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        private static readonly ILogger _log = Log.ForContext<HttpSerilogMiddleware>();

        private static readonly HashSet<string> _headerWhitelist = new()
        {
            "Content-Type",
            "Content-Length",
            "User-Agent"
        };

        private readonly RequestDelegate _next;

        #endregion

        #region Constructor

        public HttpSerilogMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        #endregion

        #region Public Methods

        public async Task Invoke(HttpContext httpContext, IIdentityProvider identityProvider, IStartTimerService startTimerService)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            long start = startTimerService.GetStartTime() == null ? Stopwatch.GetTimestamp() : startTimerService.GetStartTime().Value;
            double authElapsedTime = TimeHelper.GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());

            try
            {
                string requestBody = await GetRequestBodyAsync(httpContext);

                // TODO[MC] log response body
                using (LogContext.PushProperty("UserId", GuidHelper.FormatValue(identityProvider.UserContext?.Id)))
                using (LogContext.PushProperty("UserName", identityProvider.UserContext?.Email))
                using (LogContext.PushProperty("TenantId", GuidHelper.FormatValue(identityProvider.TenantContext?.Id)))
                using (LogContext.PushProperty("WorkspaceId", GuidHelper.FormatValue(identityProvider.ActiveWorkspace?.Id)))
                using (LogContext.PushProperty("TemplatePath", GetTemplatePath(httpContext)))
                using (LogContext.PushProperty("CorrelationId", GetCorrelationId(httpContext)))
                using (LogContext.PushProperty("AuthElapsed", authElapsedTime))
                {
                    byte[] requestData = Encoding.UTF8.GetBytes(requestBody);
                    httpContext.Request.Body = new MemoryStream(requestData);

                    await _next(httpContext);
                    double elapsedMs = TimeHelper.GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());

                    int? statusCode = httpContext.Response?.StatusCode;
                    var level = statusCode > 299 ? LogEventLevel.Error : LogEventLevel.Information;

                    var log = level == LogEventLevel.Error ? LogForErrorContext(httpContext) : _log;
                    using (LogContext.PushProperty("RequestBody", requestBody.TrimByMaxLenth()))
                    {
                        log.Write(level, MessageTemplate, httpContext.Request.Method, GetRawTarget(httpContext), statusCode, elapsedMs);
                    }
                }
            }
            // Never caught, because `LogException()` returns false.
            catch (Exception ex) when (LogException(httpContext, TimeHelper.GetElapsedMilliseconds(start, Stopwatch.GetTimestamp()), ex)) { }
        }

        #endregion

        #region Private Methods

        private static async Task<string> GetRequestBodyAsync(HttpContext context)
        {
            string requestContent = string.Empty;
            using (var reader = new StreamReader(
                       context.Request.Body,
                       Encoding.UTF8,
                       false,
                       4096,
                       true))
            {
                requestContent = await reader.ReadToEndAsync();
            }

            return requestContent;
        }

        private static bool LogException(HttpContext httpContext,
            double elapsedMs,
            Exception ex)
        {
            LogForErrorContext(httpContext)
                .Error(ex, MessageTemplate, httpContext.Request.Method, GetRawTarget(httpContext), 500, elapsedMs);

            return false;
        }

        private static ILogger LogForErrorContext(HttpContext httpContext)
        {
            var request = httpContext.Request;

            var loggedHeaders = request.Headers
                .Where(h => _headerWhitelist.Contains(h.Key))
                .ToDictionary(h => h.Key, h => h.Value.ToString());

            var result = _log
                .ForContext("RequestHeaders", loggedHeaders, true)
                .ForContext("RequestHost", request.Host)
                .ForContext("RequestProtocol", request.Protocol);

            return result;
        }

        private static string GetRawTarget(HttpContext httpContext)
        {
            return httpContext.Features.Get<IHttpRequestFeature>()?.RawTarget ?? httpContext.Request.Path.ToString();
        }

        private static string GetTemplatePath(HttpContext httpContext)
        {
            Endpoint endpointBeingHit = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;
            ControllerActionDescriptor actionDescriptor = endpointBeingHit?.Metadata?.GetMetadata<ControllerActionDescriptor>();

            return actionDescriptor?.AttributeRouteInfo.Template;
        }

        public string GetCorrelationId(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue("X-Correlation-ID", out StringValues correlationIdValues);
            string correlationId = correlationIdValues.FirstOrDefault() ?? httpContext.TraceIdentifier;

            return correlationId;
        }

        #endregion
    }
}