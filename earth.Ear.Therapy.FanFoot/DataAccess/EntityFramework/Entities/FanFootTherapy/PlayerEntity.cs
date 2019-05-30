using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy
{
    [Table("Player")]
    public class PlayerEntity
    {
        [Key]
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int TeamSequenceIndex { get; set; }
        public string PlayerTypeNameSingular { get; set; }
        public string PlayerTypeNamePlural { get; set; }
        public int PremierLeagueElementId { get; set; }
        public int PremierLeagueElementTypeId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int NowCost { get; set; }
        public int TotalPoints { get; set; }
        public string Status { get; set; }
        public string News { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
