using System;
using System.Data.SqlTypes;

namespace Blogposts.Common.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets unix epoch time in seconds
        /// </summary>
        public static double GetUnixEpoch(this DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return unixTime.TotalSeconds;
        }

        /// <summary>
        /// Calculates the number of months between two dates. The dates can be supplied
        /// in any order. We assume that each month has an average of 30 days per month
        /// rather than (365.25/12) as this causing minor rounding issues on months more than 30 days
        /// </summary>
        /// <param name="first">The first given date</param>
        /// <param name="second">the second given date</param>
        /// <returns>0 if the result is less than a month, else the number of months between the two dates</returns>
        public static int MonthsBetween(this DateTime first, DateTime second)
        {
            double days = Math.Abs((first - second).TotalDays);
            int months = (int)(days / 30);
            return months;
        }

        /// <summary>
        /// Gets the quarter of a given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetQuarter(this DateTime date)
        {
            return (date.Month + 2) / 3;
        }

        public static DateTime MaxOf(params DateTime[] dates)
        {
            DateTime max = DateTime.MinValue;
            foreach (DateTime date in dates)
            {
                if (DateTime.Compare(date, max) > 0)
                {
                    max = date;
                }
            }
            return max;
        }

        public static bool IsValidSqlDate(this DateTime date)
        {
            bool isValid = false;

            try
            {
                var sdt = new SqlDateTime(date);
                isValid = true;
            }
            catch (SqlTypeException ex)
            {
            }

            return isValid;
        }
    }
}