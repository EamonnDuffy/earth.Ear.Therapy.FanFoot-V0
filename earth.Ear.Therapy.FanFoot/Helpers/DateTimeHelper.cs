using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using earth.Ear.Therapy.FanFoot.Extensions;
using log4net;
using System;
using System.Reflection;

namespace earth.Ear.Therapy.FanFoot.Helpers
{
    public static class DateTimeHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static int GetWeekOffset(SeasonEntity seasonEntity, DateTime dateTimeUtc)
        {
            var weekOffset = -1;

            var beginWeekNumber = seasonEntity.BeginDateTimeUtc.GetIsoWeekOfYear();

            var year = dateTimeUtc.Year;

            var weekNumber = dateTimeUtc.GetIsoWeekOfYear();

            if (year == seasonEntity.BeginDateTimeUtc.Year)
            {
                weekOffset = (weekNumber - beginWeekNumber);
            }
            // ASSUMPTION: End Date Time UTC = Begin Date Time UTC + 1.
            else if (year == seasonEntity.EndDateTimeUtc.Year)
            {
                weekOffset = (52 - beginWeekNumber) + weekNumber;
            }
            else
            {
                throw new Exception($"The specified Date ({dateTimeUtc:yyyy-MMM-dd}) does not fall within the specified Season (Season Id = {seasonEntity.SeasonId}).");
            }

            return weekOffset;
        }
    }
}
