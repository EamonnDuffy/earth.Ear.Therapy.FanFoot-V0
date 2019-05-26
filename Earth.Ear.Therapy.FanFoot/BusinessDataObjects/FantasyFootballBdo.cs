using System.Collections.Generic;
using Earth.Ear.Therapy.FanFoot.DataTransferObjects.PremierLeague;

namespace Earth.Ear.Therapy.FanFoot.BusinessDataObjects
{
    public class FantasyFootballBdo
    {
        public Dictionary<int, Team> Teams { get; set; }

        public Dictionary<int, ElementType> PlayerTypes { get; set; }

        public Dictionary<int, Element> Players { get; set; }
    }
}
