namespace Blogposts.Common.Utilities.Abstractions
{
    /// <summary>
    /// Simple interface for builder pattern of any type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBuilder<out T>
    {
        T Build();
    }
}
