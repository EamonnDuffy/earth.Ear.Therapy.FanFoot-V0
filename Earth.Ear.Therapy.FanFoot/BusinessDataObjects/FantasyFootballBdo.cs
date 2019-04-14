using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Earth.Ear.Ot.FantasyFootball.DataTransferObjects.PremierLeague;

namespace Earth.Ear.Therapy.FanFoot.BusinessDataObjects
{
    public class TeamBdo : Team
    {
#if false
        public int id { get; set; }
        public IList<CurrentEventFixture> current_event_fixture { get; set; }
        public IList<NextEventFixture> next_event_fixture { get; set; }
        public string name { get; set; }
        public int code { get; set; }
        public string short_name { get; set; }
        public bool unavailable { get; set; }
        public int strength { get; set; }
        public int position { get; set; }
        public int played { get; set; }
        public int win { get; set; }
        public int loss { get; set; }
        public int draw { get; set; }
        public int points { get; set; }
        public object form { get; set; }
        public string link_url { get; set; }
        public int strength_overall_home { get; set; }
        public int strength_overall_away { get; set; }
        public int strength_attack_home { get; set; }
        public int strength_attack_away { get; set; }
        public int strength_defence_home { get; set; }
        public int strength_defence_away { get; set; }
        public int team_division { get; set; }
#endif
    }

    public class PlayerTypeBdo : ElementType
    {
#if false
        public int id { get; set; }
        public string singular_name { get; set; }
        public string singular_name_short { get; set; }
        public string plural_name { get; set; }
        public string plural_name_short { get; set; }
#endif
    }

    public class PlayerBdo : Element
    {
#if false
        public int id { get; set; }
        public string photo { get; set; }
        public string web_name { get; set; }
        public int team_code { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public int? squad_number { get; set; }
        public string news { get; set; }
        public int now_cost { get; set; }
        public DateTime? news_added { get; set; }
        public int? chance_of_playing_this_round { get; set; }
        public int? chance_of_playing_next_round { get; set; }
        public string value_form { get; set; }
        public string value_season { get; set; }
        public int cost_change_start { get; set; }
        public int cost_change_event { get; set; }
        public int cost_change_start_fall { get; set; }
        public int cost_change_event_fall { get; set; }
        public bool in_dreamteam { get; set; }
        public int dreamteam_count { get; set; }
        public string selected_by_percent { get; set; }
        public string form { get; set; }
        public int transfers_out { get; set; }
        public int transfers_in { get; set; }
        public int transfers_out_event { get; set; }
        public int transfers_in_event { get; set; }
        public int loans_in { get; set; }
        public int loans_out { get; set; }
        public int loaned_in { get; set; }
        public int loaned_out { get; set; }
        public int total_points { get; set; }
        public int event_points { get; set; }
        public string points_per_game { get; set; }
        public string ep_this { get; set; }
        public string ep_next { get; set; }
        public bool special { get; set; }
        public int minutes { get; set; }
        public int goals_scored { get; set; }
        public int assists { get; set; }
        public int clean_sheets { get; set; }
        public int goals_conceded { get; set; }
        public int own_goals { get; set; }
        public int penalties_saved { get; set; }
        public int penalties_missed { get; set; }
        public int yellow_cards { get; set; }
        public int red_cards { get; set; }
        public int saves { get; set; }
        public int bonus { get; set; }
        public int bps { get; set; }
        public string influence { get; set; }
        public string creativity { get; set; }
        public string threat { get; set; }
        public string ict_index { get; set; }
        public int ea_index { get; set; }
        public int element_type { get; set; }
        public int team { get; set; }
#endif
    }

    public class FantasyFootballBdo
    {
        public Dictionary<int, Team> Teams { get; set; }

        public Dictionary<int, ElementType> PlayerTypes { get; set; }

        public Dictionary<int, Element> Players { get; set; }
    }
}
