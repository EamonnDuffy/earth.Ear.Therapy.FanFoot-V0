using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using System;
using System.Collections.Generic;

namespace Earth.Ear.Therapy.FanFoot.Models
{
    public class SeasonViewModel
    {
        public List<SeasonEntity> LastSeasons { get; set; }

        public DateTime BeginDateTimeUtc { get; set; }
        public DateTime EndDateTimeUtc { get; set; }
    }
}
