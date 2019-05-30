using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy
{
    [Table("DatabaseVersion")]
    public class DatabaseVersionEntity
    {
        [Key]
        public int DatabaseVersionId { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int ServicePack { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
