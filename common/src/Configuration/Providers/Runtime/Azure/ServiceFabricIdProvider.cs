using Blogposts.Common.Configuration.Exceptions;
using Blogposts.Common.Configuration.Options;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Fabric;
using System.Fabric.Query;

namespace Blogposts.Common.Configuration.Providers.Runtime.Azure
{
    /// <summary>
    /// class for Azure Fabric Provider Id.
    /// Implementation of IInstanceIdProvider interface.
    /// A running service, upon startup (and at runtime)  interrogate the orchestration environment and obtain its instance Id
    /// InstanceID is the part of runtime configuration.
    //  If not running under orchestration(e.g.when in development), then get it from appsettings file.
    /// </summary>
    public class ServiceFabricIdProvider : IInstanceIdProvider
    {
        /// <summary>
        /// Set Instance Id value on creation
        /// </summary>
        public string InstanceId { get; private set; }

        /// <summary>
        /// Implemented method of getting Instance Id from settings.
        /// If InstanceId is absent in settings, then provided InstanceId from running ContainerId.
        /// </summary>
        /// <returns>Instance Id like string</returns>
        public string GetInstanceId()
        {
            if (!string.IsNullOrEmpty(InstanceId))
            {
                return InstanceId;
            }
            else
            {
                return GetContainerInstanceId();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="env">from Dependency injection of IHostingEnvironment</param>
        /// <param name="config">from Dependency injection of IConfiguration</param>
        public ServiceFabricIdProvider(IHostEnvironment env, IOptionsMonitor<RuntimeOptions> options)
        {
            if (env.IsDevelopment())
            {
                InstanceId = options.CurrentValue.InstanceId;
            }
        }

        /// <summary>
        /// Get runtime Container Instance Id in Azure Fabric of working replica.
        /// Each node has only one application this the same name.
        /// </summary>
        /// <returns>instance id</returns>
        public string GetContainerInstanceId()
        {
            //Environment information
            string partitionid = Environment.GetEnvironmentVariable("Fabric_PartitionId");
            string nodeName = Environment.GetEnvironmentVariable("Fabric_NodeName");
            string appName = Environment.GetEnvironmentVariable("Fabric_ApplicationName");

            if ((!string.IsNullOrEmpty(partitionid)) && (!string.IsNullOrEmpty(nodeName)) && (!string.IsNullOrEmpty(appName)))
            {
                using (var client = new FabricClient())
                {
                    var replicas = client.QueryManager.GetDeployedReplicaListAsync(nodeName, new Uri(appName));
                    foreach (DeployedServiceReplica partition in replicas.Result)
                    {
                        if ((!string.IsNullOrEmpty(partition.Address))
                           && partition.ReplicaStatus == ServiceReplicaStatus.Ready
                            && (partitionid == partition.Partitionid.ToString())
                            && (partition is DeployedStatelessServiceInstance))
                        {
                            return (partition as DeployedStatelessServiceInstance).InstanceId.ToString();
                        }
                    }
                }
            }

            throw new InstanceIdNotFoundException("Service Fabric variables are not present and no instance Id available in configuration");
        }
    }
}