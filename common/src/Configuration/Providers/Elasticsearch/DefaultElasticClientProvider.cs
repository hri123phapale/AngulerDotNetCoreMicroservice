using Blogposts.Common.Configuration.Options.Elasticsearch;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Nest;
using System;

namespace Blogposts.Common.Configuration.Elasticsearch
{
    public class DefaultElasticClientProvider : IElasticClientProvider
    {
        public IElasticClient Client { get; }

        public DefaultElasticClientProvider(IOptions<ElasticConnectionSettings> settings, IHostingEnvironment env, IOptions<ElasticClientProviderOptions> options)
        {

            ConnectionSettings connectionSettings = new ConnectionSettings(new Uri(settings?.Value?.ClusterUrl ?? Constants.ElasticSearch.DefaultClusterUrl));

            if (env.IsDevelopment())
            {
                connectionSettings.EnableDebugMode();
                connectionSettings.IncludeServerStackTraceOnError();
            }

            string defaultIndexName = Constants.ElasticSearch.DefaultIndexName;

            if (!string.IsNullOrWhiteSpace(defaultIndexName))
            {
                connectionSettings.DefaultIndex(defaultIndexName);
            }

            foreach (var mappingConfig in options.Value.MappingConfigurationOptions)
            {
                if (!string.IsNullOrWhiteSpace(mappingConfig.RelationName))
                {
                    connectionSettings.DefaultMappingFor(mappingConfig.Type, f => f
                        .IndexName(mappingConfig.IndexName ?? defaultIndexName)
                        .RelationName(mappingConfig.RelationName)
                        .DisableIdInference(mappingConfig.DisableIdInference));
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(mappingConfig.IndexName))
                    {
                        connectionSettings.DefaultMappingFor(mappingConfig.Type, f => f
                            .IndexName(mappingConfig.IndexName));
                    }
                }
            }

            if (settings != null && settings.Value.Security.UseBasicAuthentication)
            {
                connectionSettings.BasicAuthentication(settings.Value.Security.Username, settings.Value.Security.Password);
            }

            this.Client = new ElasticClient(connectionSettings);
        }
    }
}