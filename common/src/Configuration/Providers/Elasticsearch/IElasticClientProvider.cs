using Nest;

namespace Blogposts.Common.Configuration.Elasticsearch
{
    public interface IElasticClientProvider
    {
        IElasticClient Client { get; }
    }
}
