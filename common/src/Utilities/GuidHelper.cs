using System;

namespace Blogposts.Common.Utilities
{
    public static class GuidHelper
    {
        public static string FormatValue(Guid? guid)
        {
            return (!guid.HasValue || guid == Guid.Empty)
                ? null
                : guid.Value.ToString();
        }
    }
}