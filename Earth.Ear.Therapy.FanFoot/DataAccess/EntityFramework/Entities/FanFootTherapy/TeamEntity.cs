using System;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy
{
    public class TeamEntity
    {
        public int TeamId { get; set; }
        public int SeasonId { get; set; }
        public int WeekOffset { get; set; }
        public int PremierLeagueTeamId { get; set; }
        public string TeamName { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
