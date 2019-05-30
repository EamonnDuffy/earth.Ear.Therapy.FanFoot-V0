using System;
using System.Globalization;

namespace Earth.Ear.Therapy.FanFoot.Extensions
{
    public static class DateTimeExtensions
    {
        // Credits: https://stackoverflow.com/questions/11154673/get-the-correct-week-number-of-a-given-date
        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIsoWeekOfYear(this DateTime dateTime)
        {
            // Serious cheat. If it is Monday, Tuesday or Wednesday, then it will 
            // be the same week number as whatever Thursday, Friday or Saturday are,
            // and we always get those right.
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(dateTime);

            if ((day >= DayOfWeek.Monday) && (day <= DayOfWeek.Wednesday))
            {
                dateTime = dateTime.AddDays(3);
            }

            // Return the week of our adjusted day.
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
