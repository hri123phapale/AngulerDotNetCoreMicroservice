using System;

namespace Blogposts.Common.Utilities
{
    public abstract class HSDisposable : IHSDisposable
    {
        #region Disposable Pattern

        private bool _disposed = false;

        /// <inheritdoc/>
        public bool IsAlive => !_disposed;

        ~HSDisposable()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose logic should be placed inside this function for any items that will need to be disposed of
        /// </summary>
        protected abstract void DisposeComponent();

        /// <summary>
        /// This is part of the dispose pattern
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                DisposeComponent();
            }
            _disposed = true;
        }

        ///<inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public interface IHSDisposable : IDisposable
    {
        bool IsAlive { get; }
    }
}