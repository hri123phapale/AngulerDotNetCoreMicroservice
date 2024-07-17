namespace Blogposts.Common.Configuration.Options
{
    /// <summary>
    /// Class containing the strongly typed options necessary for the App Configuration
    /// </summary>
    public class AppConfigurationOptions
    {
        /// <summary>
        /// The connection String for the App Configuration Service
        /// </summary>
        public string ConnectionString { get; set; }
    }
}