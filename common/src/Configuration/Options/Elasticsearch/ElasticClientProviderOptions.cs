using System.Collections.Generic;

namespace Blogposts.Common.Configuration.Options.Elasticsearch
{
    public class ElasticClientProviderOptions
    {
        public IEnumerable<MappingConfigurationOptions> MappingConfigurationOptions { get; set; }

        public ElasticClientProviderOptions()
        {
            MappingConfigurationOptions = new List<MappingConfigurationOptions>();
        }
    }
}