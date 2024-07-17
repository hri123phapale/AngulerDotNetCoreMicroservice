using System;
using System.Runtime.Serialization;

namespace Blogposts.Common.Configuration.Providers.Configuration
{
    /// <summary>
    ///  Exception is thrown when Configuration is not found 
    /// </summary>
    [Serializable]
    public class ConfigurationNotFoundException : Exception
    {
        public ConfigurationNotFoundException()
        {
        }

        public ConfigurationNotFoundException(string message) : base(message)
        {
        }

        public ConfigurationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigurationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}