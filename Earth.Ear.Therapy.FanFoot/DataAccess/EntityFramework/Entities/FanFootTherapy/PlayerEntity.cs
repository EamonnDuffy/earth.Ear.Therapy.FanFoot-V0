using System;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy
{
    public class PlayerEntity
    {
        public int PlayerId { get; set; }
        public int SeasonId { get; set; }
        public int WeekOffset { get; set; }
        public int PremierLeagueTeamId { get; set; }
        public int PremierLeagueElementId { get; set; }
        public int PremierLeagueElementTypeId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string NowCost { get; set; }
        public int TotalPoints { get; set; }
        public string Status { get; set; }
        public string News { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
