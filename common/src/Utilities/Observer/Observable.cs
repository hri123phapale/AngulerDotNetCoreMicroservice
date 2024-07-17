using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogposts.Common.Utilities.Observer
{
    public class Observable<TData>
    {
        private List<IHSObserver<TData>> _observers = new List<IHSObserver<TData>>();
        private TData _subject;

        /// <summary>
        /// Registers an observer with the observable
        /// </summary>
        public void Register(IHSObserver<TData> observer)
        {
            _observers.Add(observer);
        }

        /// <summary>
        /// Unregisters an observer from the observable
        /// </summary>
        public void Unregister(IHSObserver<TData> observer)
        {
            _observers.Remove(observer);
        }

        /// <summary>
        /// Emits a new subject on the stream
        /// </summary>
        public async Task Emit(TData subject)
        {
            _subject = subject;
            await Notify();
        }

        /// <summary>
        /// Notifys all observers of a new subject
        /// </summary>
        public async Task Notify()
        {
            foreach (var observer in _observers)
            {
                await observer.Update(_subject);
            }
        }
    }
}