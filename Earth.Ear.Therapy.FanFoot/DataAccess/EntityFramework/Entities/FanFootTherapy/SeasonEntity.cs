using System;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy
{
    public class SeasonEntity
    {
        public int SeasonId { get; set; }
        public DateTime BeginDateTimeUtc { get; set; }
        public DateTime EndDateTimeUtc { get; set; }
        public DateTime CreateDateTimeUtc { get; set; }
    }
}
