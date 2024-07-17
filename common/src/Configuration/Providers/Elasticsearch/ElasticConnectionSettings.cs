namespace Blogposts.Common.Configuration.Elasticsearch
{
    public class ElasticConnectionSettings
    {
        public bool IndexingEnabled { get; set; } = true;

        public string ClusterUrl { get; set; }

        public SecurityOptions Security { get; set; }

        public string DefaultIndex
        {
            get { return this._defaultIndex; }
            set { this._defaultIndex = value.ToLower(); }
        }

        private string _defaultIndex;
    }
}