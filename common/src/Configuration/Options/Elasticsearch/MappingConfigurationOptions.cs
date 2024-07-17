using System;

namespace Blogposts.Common.Configuration.Options.Elasticsearch
{
    /// <summary>
    /// Accepts a collection used to specify data for type mappings during index creation.
    /// </summary>
    public class MappingConfigurationOptions
    {
        public string IndexName { get; set; }

        public Type Type { get; set; }

        /// <summary>
        /// Use this only when mapping parent types.
        /// </summary>
        public string RelationName { get; set; } = "DefaultRelation";

        public string TypeName { get; set; }

        public bool DisableIdInference { get; set; } = false;
    }
}