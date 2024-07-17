using System;
using System.Collections.Generic;
using System.Data;
using Assets.Configuration.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AnalysisServices.AdomdClient;

namespace Assets.Configuration
{
    // TODO: move interface and this implementation into Common.Data
    public class AnalysisServicesClientProvider : IAnalysisServicesClientProvider
    {
        private readonly IOptions<AnalysisServicesSettings> _options;
        private readonly ILogger<AnalysisServicesClientProvider> _logger;
        //private const string DefaultConnectionString = @"Data Source=http://localhost/OLAP/msmdpump.dll;Catalog=AssetsAnalysis_Tatiana_20f0f96f-77a9-4fdd-b5c7-cf9a571d5757;";

        public AnalysisServicesClientProvider(IOptions<AnalysisServicesSettings> options, ILogger<AnalysisServicesClientProvider> logger)
        {
            _options = options;
            _logger = logger;
        }
        
        public List<KeyValuePair<T, long>> ExecuteAndFill<T>(string commandText, Func<IDataReader, KeyValuePair<T, long>> func)
        {
            var reader = ExecuteQuery(commandText);

            var result = new List<KeyValuePair<T, long>>();

            while (reader.Read())
            {
                result.Add(func(reader));
            }

            reader.Close();

            return result;
        }

        private IDataReader ExecuteQuery(string command)
        {
            var connectionString = GetConnectionString();

            var connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connectionString);
            
            connection.Open();
            
            return new AdomdCommand(command, connection).ExecuteReader(CommandBehavior.CloseConnection);
        }

        private string GetConnectionString()
        {
            return _options.Value?.ConnectionString ?? throw new Exception("No connection string found.");
            //_logger.LogWarning("No connection string found in config, using default (hardcoded) one");
            //return DefaultConnectionString;
        }
    }
}
