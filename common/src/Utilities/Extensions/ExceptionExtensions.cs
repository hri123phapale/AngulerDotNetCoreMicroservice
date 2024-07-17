using System;

namespace Blogposts.Common.Utilities.Extensions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Gets inner most exception
        /// </summary>
        public static Exception GetInnerMostException(this Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            return ex;
        }
    }
}