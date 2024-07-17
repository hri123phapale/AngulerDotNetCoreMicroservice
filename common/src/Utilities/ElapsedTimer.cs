using System;
using System.Timers;

namespace Blogposts.Common.Utilities
{
    /// <summary>
    /// Class that initializes a timer with the specified interval and raises an event when this timer elapses
    /// </summary>
    public class ElapsedTimer : IDisposable
    {
        public delegate void TimerElapsedDelegate();

        public event TimerElapsedDelegate TimerElapsed;

        private readonly Timer _timer;

        public ElapsedTimer(int interval)
        {
            _timer = new Timer(interval);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = false;
        }

        /// <summary>
        /// Resets the timer
        /// </summary>
        public void ResetTimer()
        {
            _timer.Stop();
            _timer.Start();
        }

        /// <summary>
        /// Triggered when the interval elapses to raise the timer elapsed event
        /// </summary>
        /// <param name="source">The source object</param>
        /// <param name="e">The event arguments</param>
        private void OnTimerElapsed(object source, ElapsedEventArgs e)
        {
            TimerElapsed?.Invoke();
        }

        #region Dispose pattern

        private bool _disposedValue = false;

        ~ElapsedTimer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _timer.Elapsed -= OnTimerElapsed;
                    _timer.Dispose();
                }

                _disposedValue = true;
            }
        }

        #endregion
    }
}