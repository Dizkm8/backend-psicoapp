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
        /// Valida if the Date provided is in the current or a greater week
        /// </summary>
        /// <param name="date">Date to check</param>
        /// <returns>True if its valid, otherwise false</returns>
        public static bool DateIsOnThisWeekOrGreater(DateOnly date)
        {
            var mondayCurrentWeek = GetMondayCurrentWeek();
            return date >= mondayCurrentWeek;
        }

        public static DateOnly GetMondayCurrentWeek()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now.Date);
            var index = GetDayIndex(currentDate);
            var displacedDate = currentDate.AddDays(-index);
            return displacedDate;
        }

        /// <summary>
        /// Validates if the date provided is not in a week greater than the 
        /// the current week + the amount of weeks provided
        /// </summary>
        /// <param name="date">Date to check</param>
        /// <param name="weekAmount">Number max of weeks based on current week</param>
        /// <returns>True if its valid. otherwise null</returns>
        public static bool DateIsNotGreaterThanWeek(DateOnly date, int weekAmount)
        {
            if(weekAmount < 0) return false;
            var mondayCurrentWeek = GetMondayCurrentWeek();
            // We are going tocompare to the first monday over the limit,
            // so add a week to the limit
            weekAmount++;
            var mondayWeekLimit = mondayCurrentWeek.AddDays(weekAmount * 7);
            // Necesary be strictly minor than the limit
            return date < mondayWeekLimit;
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
            return DateIsNotGreaterThanWeek(date, weeksAmount);
        }
    }
}