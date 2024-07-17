namespace Blogposts.Common.Configuration.Options
{
    /// <summary>
    /// Class containing the strongly typed options necessary for the runtime
    /// </summary>
    public class RuntimeOptions
    {
        /// <summary>
        /// The instance id
        /// </summary>
        public string InstanceId { get; set; }

        /// <summary>
        /// Indicates if service configuration is used
        /// </summary>
        public bool? UseServiceConfiguration { get; set; }

        /// <summary>
        /// Indicates if the activation flow that will send the user the activation email is enabled
        /// </summary>
        public bool? EnableActivation { get; set; }

        /// <summary>
        /// Enables the merging of Backoffice Shell with the Login Shell
        /// Used only on hosted environments - not on development
        /// </summary>
        public bool? EnableSingleBackOfficeLogin { get; set; }

        /// <summary>
        /// The region where the runtime is deployed
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// The interval for the progress report generation will be called (interval in milliseconds)
        /// </summary>
        public int ReportGenerationProgressInterval { get; set; } = 3000;
    }
}