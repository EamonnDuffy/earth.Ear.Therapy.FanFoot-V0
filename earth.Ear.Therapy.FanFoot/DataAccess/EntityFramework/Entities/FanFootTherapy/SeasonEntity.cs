using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy
{
    [Table("Season")]
    public class SeasonEntity
    {
        [Key]
        public int SeasonId { get; set; }
        public DateTime BeginDateTimeUtc { get; set; }
        public DateTime EndDateTimeUtc { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public DateTime UpdatedDateTimeUtc { get; set; }
    }
}
