using earth.Ear.Therapy.FanFoot.DataTransferObjects.PremierLeague;
using System.Collections.Generic;

namespace earth.Ear.Therapy.FanFoot.BusinessDataObjects
{
    public class FantasyFootballBdo
    {
        public Dictionary<int, Team> Teams { get; set; }

        public Dictionary<int, ElementType> PlayerTypes { get; set; }

        public Dictionary<int, Element> Players { get; set; }
    }
}
