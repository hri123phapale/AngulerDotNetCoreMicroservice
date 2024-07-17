using System.Threading.Tasks;

namespace Blogposts.Common.Utilities.Observer
{
    public interface IHSObserver<TData>
    {
        /// <summary>
        /// Updates the observer with the latest subject
        /// </summary>
        Task Update(TData data);
    }
}