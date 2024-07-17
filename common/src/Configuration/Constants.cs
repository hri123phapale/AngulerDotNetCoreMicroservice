namespace Blogposts.Common.Configuration
{
    public static class Constants
    {
        public static class ElasticSearch
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "URL used as a sensible default.")]
            public const string DefaultClusterUrl = "http://localhost:9200";

            public const string DefaultIndexName = "defaultIndex";
        }
    }
}
