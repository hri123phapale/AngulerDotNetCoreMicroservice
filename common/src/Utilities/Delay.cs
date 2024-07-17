using System;
using System.Timers;

namespace Blogposts.Common.Utilities
{
    /// <summary>
    /// Delay will run an action after a specified delay
    /// </summary>
    public static class Delay
    {
        /// <summary>
        /// Runs an action after a delay
        /// </summary>
        /// <param name="after">time in milliseconds after which to execute the action</param>
        /// <param name="action">the action to execute</param>
        public static void Do(int after, Action action)
        {
            if (after <= 0 || action == null)
            {
                return;
            }

            var timer = new Timer { Interval = after, Enabled = false };

            timer.Elapsed += (sender, e) =>
            {
                timer.Stop();
                action.Invoke();
                timer.Dispose();
            };

            timer.Start();
        }
    }
}