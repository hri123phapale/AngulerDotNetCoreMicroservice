using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blogposts.Common.Utilities
{
    /// <summary>
    /// Timer wrapper for predeterminded timeouts
    /// </summary>
    public class HsTimer : IDisposable
    {
        /// <summary>
        /// The duration of the timer before the callback gets invoked
        /// </summary>
        public int Duration { get; set; }

        private Timer Timer { get; set; }

        private Action Callback { get; set; }

        /// <summary>
        /// Async callback
        /// </summary>
        private Func<Task> CallbackAsync { get; set; }

        private DateTime DueTime { get; set; }

        public HsTimer(Action callback) : this()
        {
            Callback = callback;
        }

        public HsTimer(Func<Task> callbackAsync) : this()
        {
            CallbackAsync = callbackAsync;
        }

        public HsTimer()
        {
            Timer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// Remaining time on the timer in milliseconds
        /// </summary>
        public double RemainingMilliseconds
        {
            get
            {
                double milliseconds = (DueTime - DateTime.UtcNow).TotalMilliseconds;
                return milliseconds > 0 ? milliseconds : 0;
            }
        }

        /// <summary>
        /// Flag indicates if the timer has remaining time
        /// </summary>
        public bool HasRemainingTime
        {
            get => RemainingMilliseconds < 0;
        }

        /// <summary>
        /// This will start the timer for the duration of the duration value
        /// </summary>
        /// <param name="duration"></param>
        public async Task Start(int duration)
        {
            Duration = duration <= 0 ? 0 : duration;
            DueTime = DateTime.UtcNow.AddMilliseconds(Duration);
            if (Duration == 0)
            {
                Callback?.Invoke();
                if (CallbackAsync != null)
                {
                    await CallbackAsync.Invoke();
                }
            }
            else
            {
                Timer?.Change(Duration, Timeout.Infinite);
            }
        }

        /// <summary>
        /// This will stop the timer execution
        /// </summary>
        public void Stop()
        {
            Duration = 0;
            Timer?.Change(Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// Called by the disposable pattern to dispose of the timer
        /// </summary>
        protected void DisposeComponent()
        {
            Stop();
            Timer.Dispose();
            Timer = null;
            Callback = null;
            CallbackAsync = null;
        }

        private void TimerCallback(object state)
        {
            Callback?.Invoke();
            if (CallbackAsync != null)
            {
                CallbackAsync?.Invoke();
            }
        }

        #region Dispose pattern

        private bool _disposedValue = false;

        ~HsTimer()
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
                    DisposeComponent();
                }

                _disposedValue = true;
            }
        }

        #endregion
    }
}