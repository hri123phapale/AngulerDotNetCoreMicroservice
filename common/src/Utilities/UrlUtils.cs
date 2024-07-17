using Flurl;

namespace Blogposts.Common.Utilities
{
    public static class UrlUtils
    {
        public static string Combine(params string[] components)
        {
            return Url.Combine(components);
        }
    }
}