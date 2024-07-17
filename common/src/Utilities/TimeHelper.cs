using System.Diagnostics;

namespace Blogposts.Common.Utilities
{
    /// <summary>
    /// COntains various helper functions related with Time
    /// </summary>
    public static class TimeHelper
    {
        /// <summary>
        /// Get Elapsed time in ms
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static double GetElapsedMilliseconds(long start, long stop)
        {
            return (stop - start) * 1000 / (double)Stopwatch.Frequency;
        }
    }
}