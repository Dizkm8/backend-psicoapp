namespace PsicoAppAPI.Util
{
    public class DateHelper
    {
        /// <summary>
        /// Gets the index of the day of the week
        /// where Monday = 0 and Sunday = 6
        /// </summary>
        /// <param name="date">Date to obtain index</param>
        /// <returns>Date index</returns>
        public static int GetDayIndex(DateOnly date)
        {
            // Week start with sunday = 0;
            var dayIndex = (int)date.DayOfWeek;
            // Reduce all index to start with monday = 0;
            dayIndex--;
            // Fixes sunday index;
            if (dayIndex == -1) dayIndex = 6;
            return dayIndex;
        }

        /// <summary>
        /// Displaces the current date the amount of days provided 
        /// </summary>
        /// <param name="index">Amount to displace</param>
        /// <returns>Date displaced</returns>
        public static DateOnly GetDisplacedDay(int index)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now.Date);
            var displacedDate = currentDate.AddDays(index);
            return displacedDate;
        }

        /// <summary>
        /// Valida if the Date provided is in the current or a greater week
        /// </summary>
        /// <param name="date">Date to check</param>
        /// <returns>True if its valid, otherwise false</returns>
        public static bool DateIsOnThisWeekOrGreater(DateOnly date)
        {
            //Get the index of the day
            var index = GetDayIndex(date);
            // Displaces the current date to the start of the week
            var startOfWeekDate = GetDisplacedDay(-index);
            // Compare the provided date to the monday of the week
            // If the provided date is less, means that the date is not in the same week
            return date >= startOfWeekDate;
        }

        /// <summary>
        /// Validates if the date provided is not in a week greater the 
        /// the current week + the amount of weeks provided
        /// </summary>
        /// <param name="date">Date to check</param>
        /// <param name="weekAmount">Number max of weeks based on current week</param>
        /// <returns>True if its valid. otherwise null</returns>
        public static bool DateIsNotGreaterThankWeek(DateOnly date, int weekAmount)
        {
            if (weekAmount < 0) return false;
            var relativeSunday = GetLastDayOftheWeek(date);
            relativeSunday = relativeSunday.AddDays(weekAmount * 7);
            return relativeSunday > date;
        }

        /// <summary>
        /// Get the date of the sunday of the week of the date provideds
        /// </summary>
        /// <param name="date">Date to check</param>
        /// <returns>Relative sunday date</returns>
        private static DateOnly GetLastDayOftheWeek(DateOnly date)
        {
            //Get the index of the day
            var index = GetDayIndex(date);
            // Obtain the index needed to display to sunday
            var displaceToEndWeekIndex = 6 - index;
            // Get the sunday date of the week from date provided
            return GetDisplacedDay(displaceToEndWeekIndex);
        }

        /// <summary>
        /// Check if a date provided is in the current week or greater
        /// and less or equals than the weeksAmount provided
        /// </summary>
        /// <param name="date">Date to check</param>
        /// <param name="weeksAmount">Maximum weeks amount limit</param>
        /// <returns>True if its on range</returns>
        public static bool DateIsOnWeekRange(DateOnly date, int weeksAmount)
        {
            if (!DateIsOnThisWeekOrGreater(date)) return false;
            return DateIsNotGreaterThankWeek(date, weeksAmount);
        }
    }
}