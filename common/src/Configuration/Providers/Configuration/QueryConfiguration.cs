using ServiceStack;

namespace Blogposts.Common.Configuration.Providers.Configuration
{
    /// <summary>
    /// Generation of route to remote configuration API
    /// </summary>
    [Route("api/v1/service-configuration-api/configuration")]
    internal class QueryConfiguration : IReturn<string>
    {
        /// <summary>
        /// Dev, Test, Staging, or Production. 
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// typeof(Program).Namespace
        /// </summary>
        public string Namespace { get; set; }
    }
}

