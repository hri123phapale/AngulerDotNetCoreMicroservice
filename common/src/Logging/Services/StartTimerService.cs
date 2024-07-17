using System.Diagnostics;

namespace Blogposts.Common.Logging.Services
{
    public class StartTimerService : IStartTimerService
    {
        #region Private Members

        private long? _start;

        #endregion

        #region Public Methods

        public long? GetStartTime() => _start;

        public void StartTimer() => _start = Stopwatch.GetTimestamp();

        #endregion
    }
}