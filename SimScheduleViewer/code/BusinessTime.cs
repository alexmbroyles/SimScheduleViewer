using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimScheduleViewer.code
{
    public static class businessHours
    {
        /// <summary>
        /// Adds the given number of business days to the <see cref="DateTime"/>.
        /// </summary>
        /// <param name="current">The date to be changed.</param>
        /// <param name="days">Number of business days to be added.</param>
        /// <returns>A <see cref="DateTime"/> increased by a given number of business days.</returns>
        public static DateTime AddBusinessDays(this DateTime current, int days)
        {
            var sign = Math.Sign(days);
            var unsignedDays = Math.Abs(days);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    current = current.AddDays(sign);
                }
                while (current.DayOfWeek == DayOfWeek.Saturday ||
                    current.DayOfWeek == DayOfWeek.Sunday);
            }
            return current;
        }

        /// <summary>
        /// Subtracts the given number of business days to the <see cref="DateTime"/>.
        /// </summary>
        /// <param name="current">The date to be changed.</param>
        /// <param name="days">Number of business days to be subtracted.</param>
        /// <returns>A <see cref="DateTime"/> increased by a given number of business days.</returns>
        public static DateTime SubtractBusinessDays(this DateTime current, int days)
        {
            return AddBusinessDays(current, -days);
        }
        public static int GetBusinessDays(DateTime start, DateTime end)
        {
            if (start.DayOfWeek == DayOfWeek.Saturday)
            {
                start = start.AddDays(2);
            }
            else if (start.DayOfWeek == DayOfWeek.Sunday)
            {
                start = start.AddDays(1);
            }

            if (end.DayOfWeek == DayOfWeek.Saturday)
            {
                end = end.AddDays(-1);
            }
            else if (end.DayOfWeek == DayOfWeek.Sunday)
            {
                end = end.AddDays(-2);
            }

            int diff = (int)end.Subtract(start).TotalDays;

            int result = diff / 7 * 5 + diff % 7;

            if (end.DayOfWeek < start.DayOfWeek)
            {
                return result - 2;
            }
            else
            {
                return result;
            }
        }
        public static DateTime AddBusinessDays2(DateTime date, int days)
        {
            if (days < 0)
            {
                throw new ArgumentException("days cannot be negative", "days");
            }

            if (days == 0) return date;

            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(2);
                days -= 1;
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
                days -= 1;
            }

            date = date.AddDays(days / 5 * 7);
            int extraDays = days % 5;

            if ((int)date.DayOfWeek + extraDays > 5)
            {
                extraDays += 2;
            }

            return date.AddDays(extraDays);

        }
        public static TimeSpan BusinessTimeDelta(DateTime start, DateTime stop)
        {
            if (start == stop)
                return TimeSpan.Zero;

            if (start > stop)
            {
                DateTime temp = start;
                start = stop;
                stop = temp;
            }

            // First we are going to truncate these DateTimes so that they are within the business day.

            // How much time from the beginning til the end of the day?
            DateTime startFloor = StartOfBusiness(start);
            DateTime startCeil = CloseOfBusiness(start);
            if (start < startFloor) start = startFloor;
            if (start > startCeil) start = startCeil;

            TimeSpan firstDayTime = startCeil - start;
            bool workday = true; // Saves doublechecking later
            if (!IsWorkday(start))
            {
                workday = false;
                firstDayTime = TimeSpan.Zero;
            }

            // How much time from the start of the last day til the end?
            DateTime stopFloor = StartOfBusiness(stop);
            DateTime stopCeil = CloseOfBusiness(stop);
            if (stop < stopFloor) stop = stopFloor;
            if (stop > stopCeil) stop = stopCeil;

            TimeSpan lastDayTime = stop - stopFloor;
            if (!IsWorkday(stop))
                lastDayTime = TimeSpan.Zero;

            // At this point all dates are snipped to within business hours.

            if (start.Date == stop.Date)
            {
                if (!workday) // Precomputed value from earlier
                    return TimeSpan.Zero;

                return stop - start;
            }

            // At this point we know they occur on different dates, so we can use
            // the offset from SOB and COB.

            TimeSpan timeInBetween = TimeSpan.Zero;
            TimeSpan hoursInAWorkday = (startCeil - startFloor);

            // I tried cool math stuff instead of a for-loop, but that leaves no clean way to count holidays.
            for (DateTime itr = startFloor.AddDays(1); itr < stopFloor; itr = itr.AddDays(1))
            {
                if (!IsWorkday(itr))
                    continue;

                // Otherwise, it's a workday!
                timeInBetween += hoursInAWorkday;
            }

            return firstDayTime + lastDayTime + timeInBetween;
        }

        public static bool IsWorkday(DateTime date)
        {
            // Weekend
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return false;

            // Could add holiday logic here.

            return true;
        }

        public static DateTime StartOfBusiness(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 6, 0, 0);
        }

        public static DateTime CloseOfBusiness(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 16, 0, 0);
        }
    }
}
