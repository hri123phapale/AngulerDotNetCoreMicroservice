namespace Blogposts.Common.Configuration.Providers.Runtime

{
    /// <summary>
    /// Interface provide instance Id of container at runtime
    /// </summary>
    public interface IInstanceIdProvider
    {
        /// <summary>
        /// provide an instance Id of current container
        /// Id developer mode get value from configuration "InstanceId"
        /// </summary>
        /// <returns>instance Id</returns>
        string GetInstanceId();
    }
}