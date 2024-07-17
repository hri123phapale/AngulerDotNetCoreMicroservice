using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Linq;

namespace Blogposts.Common.Logging
{
    /// <summary>
    /// Configuration for Blogposts.Common.Logging
    /// </summary>
    public static class LogConfiguration
    {
        /// <summary>
        /// Configure logging with settings from the config file, fallback on hardcoded defaults if no file found
        /// </summary>
        public static void Configure(IConfigurationRoot config)
        {
            if (config != null && config.Providers.Any(p => p.GetType() == typeof(JsonConfigurationProvider)))
            {
                Log.Logger = GetBasicLoggerConfiguration()
                    .ReadFrom.Configuration(config)
                    .CreateLogger();
            }
            else
            {
                Configure();
            }
        }

        /// <summary>
        /// Configure logging with settings from the configuration
        /// </summary>
        public static void Configure(IConfiguration config)
        {
            Log.Logger = GetBasicLoggerConfiguration()
                 .ReadFrom.Configuration(config)
                 .CreateLogger();
        }

        /// <summary>
        /// Configure logging with hardcoded defaults.
        /// </summary>
        public static void Configure()
        {
            Log.Logger = GetBasicLoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.Debug()
                .WriteTo.File("logs\\log.txt",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss.fff} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}")
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        private static LoggerConfiguration GetBasicLoggerConfiguration()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug();
        }
    }
}