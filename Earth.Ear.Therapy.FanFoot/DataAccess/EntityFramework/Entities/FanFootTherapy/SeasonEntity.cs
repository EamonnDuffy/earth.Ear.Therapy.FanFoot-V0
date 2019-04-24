using System;
using Spire.Doc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy
{
    [Table("Season")]
    public class SeasonEntity
    {
        [Key]
        public int SeasonId { get; set; }
        public DateTime BeginDateTimeUtc { get; set; }
        public DateTime EndDateTimeUtc { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
