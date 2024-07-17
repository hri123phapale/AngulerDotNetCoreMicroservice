using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blogposts.Common.Utilities
{
    /// <summary>
    /// Allows the running of a task repeated based on the timespan.
    /// It will execute the task repeatedly until cancel is requested on the cancellation token
    /// </summary>
    public static class RepeatTask
    {
        /// <summary>
        /// Will repeat an async action every period
        /// </summary>
        /// <param name="action">The async function to call repeats based on the timespan</param>
        /// <param name="period">The timespan on which to repeat the function</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to stop execution of the repeating task</param>
        public static void Repeat(Func<Task> action, TimeSpan period, CancellationToken cancellationToken)
        {
            if (action == null)
            {
                return;
            }

            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await action();
                    await Task.Delay(period, cancellationToken);
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Repeats an async action every period, this also takes a startdelay, and will delay the start
        /// initially just once before it starts repeating based on the <see cref="TimeSpan"/> period value
        /// </summary>
        /// <param name="action">The async function to be repeated</param>
        /// <param name="period">The repreat period <see cref="TimeSpan"/></param>
        /// <param name="startDelay">The initial start delay gets used once before the repeating starts <see cref="TimeSpan"/></param>
        /// <param name="cancellationToken">The cancellation token used to cancel the repeating task</param>
        public static void RepeatAsync(Func<Task> action, TimeSpan period, TimeSpan startDelay, CancellationToken cancellationToken)
        {
            if (action == null)
            {
                return;
            }
            bool hasStartDelayRun = false;

            Task.Run(async () =>
            {
                if (!hasStartDelayRun)
                {
                    await Task.Delay(startDelay);
                    hasStartDelayRun = true;
                }
                while (!cancellationToken.IsCancellationRequested)
                {
                    await action();
                    await Task.Delay(period, cancellationToken);
                }
            }, cancellationToken);
        }
    }
}