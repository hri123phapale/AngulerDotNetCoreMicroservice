namespace Blogposts.Common.Utilities.Extensions
{
    public static class CharExtensions
    {
        public static bool IsSpecial(this char source)
        {
            return !(char.IsLetterOrDigit(source) || char.IsWhiteSpace(source));
        }
    }
}