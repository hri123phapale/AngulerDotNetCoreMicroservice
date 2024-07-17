namespace Blogposts.Common.Logging.Services
{
    public interface IStartTimerService
    {
        /// <summary>
        /// Returns the start time of the request
        /// </summary>
        /// <returns></returns>
        public long? GetStartTime();

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void StartTimer();
    }
}