using System;

namespace Blogposts.Common.Utilities
{
    public static class GenericFactory
    {
        public static TInstance GetInstance<TInstance>() where TInstance : class
        {
            return (TInstance)Activator.CreateInstance(typeof(TInstance));
        }

        public static TInstance GetInstance<TInstance>(params object[] args) where TInstance : class
        {
            return (TInstance)Activator.CreateInstance(typeof(TInstance), args);
        }

        public static object GetInstance(Type instanceType)
        {
            return Activator.CreateInstance(instanceType);
        }
    }
}