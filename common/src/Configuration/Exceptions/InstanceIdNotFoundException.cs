using System;

namespace Blogposts.Common.Configuration.Exceptions
{
    /// <summary>
    /// Exception is thrown when instance Id is not found 
    /// </summary>
    [Serializable()]
    public class InstanceIdNotFoundException : Exception
    {
        public InstanceIdNotFoundException()
        {
        }

        public InstanceIdNotFoundException(string message)
            : base(message)
        {
        }

        public InstanceIdNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InstanceIdNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }
}
