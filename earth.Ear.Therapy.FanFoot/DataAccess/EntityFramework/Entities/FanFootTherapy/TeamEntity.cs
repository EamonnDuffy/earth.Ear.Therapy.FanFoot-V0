using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy
{
    [Table("Team")]
    public class TeamEntity
    {
        [Key]
        public int TeamId { get; set; }
        public int SeasonId { get; set; }
        public int WeekOffset { get; set; }
        public int PremierLeagueTeamId { get; set; }
        public string TeamName { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
