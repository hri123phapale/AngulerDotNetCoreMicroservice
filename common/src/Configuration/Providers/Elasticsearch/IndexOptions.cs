namespace Blogposts.Common.Configuration.Elasticsearch
{
    /// <summary>
    /// Defines options related to Elasticsearch Indexes.
    /// </summary>
    public class IndexOptions
    {
        /// <summary>
        /// Specifies the prefix used in Index names. Typically the value is the environment name to which the index belongs (e.g. Test, Production).
        /// </summary>
        /// <remarks>The prefix is used to distinguish indices on the same server, which have the same name but belong to different environments.</remarks>
        public string IndexNamePrefix { get; set; }
    }
}
