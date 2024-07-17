namespace Blogposts.Common.Configuration.Elasticsearch
{
    /// <summary>
    /// Specifies credentials for basic authentication to an Elasticsearch cluster
    /// </summary>
    public class SecurityOptions
    {
        /// <summary>
        /// If set to true, basic authentication will be attempted using username/password credentials.
        /// </summary>
        public bool UseBasicAuthentication { get; set; }

        /// <summary>
        /// The username to use for basic authentication to Elasticsearch host.
        /// </summary>
        /// <remarks>The value of this field is only used if <see cref="UseBasicAuthentication"/> is set to true.</remarks>
        public string Username { get; set; }


        /// <summary>
        /// The password to use for basic authentication to Elasticsearch host.
        /// </summary>
        /// <remarks>The value of this field is only used if <see cref="UseBasicAuthentication"/> is set to true.</remarks>
        public string Password { get; set; }
    }
}
