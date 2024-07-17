namespace Blogposts.Common.Utilities.Abstractions
{
    public interface ISynchronizationBarrier
    {
        /// <summary>
        /// Signals the start of the synchronization. All <see cref="Wait"/> operations will be halted until <see cref="Finish"/> is called
        /// </summary>
        void Start();

        /// <summary>
        /// Signals the start of the synchronization. All <see cref="Wait"/> operations will be halted until <see cref="Finish"/> is called or until the <paramref name="timeoutMilliseconds"/> has elapsed
        /// </summary>
        void Start(int timeoutMilliseconds);

        /// <summary>
        /// Signals the end of the synchronization. All <see cref="Wait"/> operation will continue.
        /// </summary>
        void Finish();

        /// <summary>
        /// Waits for the <see cref="Finish"/> to be called, to continue the operation
        /// </summary>
        void Wait();
    }
}