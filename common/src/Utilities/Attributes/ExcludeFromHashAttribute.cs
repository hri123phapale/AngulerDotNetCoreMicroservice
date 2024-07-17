using System;

namespace Blogposts.Common.Utilities.Attributes
{
    /// <summary>
    /// Property marker attribute to exclude properties on the model
    /// from being added into the hash calculation
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcludeFromHashAttribute : Attribute
    {
    }
}