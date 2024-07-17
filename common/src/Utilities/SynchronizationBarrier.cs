using Blogposts.Common.Utilities.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Blogposts.Common.Utilities
{
    public class SynchronizationBarrier : ISynchronizationBarrier
    {
        private bool _isStarted;
        private bool _isFinished;
        private ManualResetEvent _waitEvent;
        private CancellationTokenSource _timeoutCancellation;

        public SynchronizationBarrier()
        {
            _isStarted = false;
            _isFinished = false;
            _waitEvent = new ManualResetEvent(false);
            _timeoutCancellation = new CancellationTokenSource();
        }

        public void Start()
        {
            Start(Timeout.Infinite);
        }

        public void Start(int timeoutMilliseconds)
        {
            if (_isStarted)
            {
                return;
            }

            lock (_waitEvent)
            {
                if (_isStarted)
                {
                    return;
                }

                _waitEvent.Reset();
                _isStarted = true;
                _isFinished = false;

                if (timeoutMilliseconds != Timeout.Infinite)
                {
                    _timeoutCancellation.CancelAfter(timeoutMilliseconds);
                    _ = WaitForTimeoutAsync(_timeoutCancellation.Token);
                }
            }
        }

        public void Finish()
        {
            lock (_waitEvent)
            {
                if (!_isStarted || _isFinished)
                {
                    return;
                }

                _isFinished = true;
                _isStarted = false;
                _waitEvent.Set();
                _timeoutCancellation.Cancel();
            }
        }

        public void Wait()
        {
            lock (_waitEvent)
            {
                if (!_isStarted || _isFinished)
                {
                    return;
                }
            }

            _waitEvent.WaitOne();
        }

        private async Task WaitForTimeoutAsync(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
                Finish();
            }
            catch (TaskCanceledException)
            {
            }
        }
    }
}