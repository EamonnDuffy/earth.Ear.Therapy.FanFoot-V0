using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Earth.Ear.Therapy.FanFoot.BusinessDataObjects;
using Earth.Ear.Therapy.FanFoot.DataTransferObjects.PremierLeague;
using Earth.Ear.Therapy.FanFoot.WebApi;

namespace Earth.Ear.Therapy.FanFoot.External
{
    public static class PremierLeague
    {
        // TODO: Implement Caching.

        // Introduce AutoMapper.

        private static Dictionary<int, Team> ProcessTeams(FantasyFootballDto responseDto)   
        {
            var dictionary = new Dictionary<int, Team>();

            foreach (var team in responseDto.teams)
            {
                dictionary[team.id] = team;
            }

            return dictionary;
        }

        private static Dictionary<int, ElementType> ProcessPlayerTypes(FantasyFootballDto responseDto)
        {
            var dictionary = new Dictionary<int, ElementType>();

            foreach (var playerType in responseDto.element_types)
            {
                dictionary[playerType.id] = playerType;
            }

            return dictionary;
        }

        private static Dictionary<int, Element> ProcessPlayers(FantasyFootballDto responseDto)
        {
            var dictionary = new Dictionary<int, Element>();

            foreach (var player in responseDto.elements)
            {
                dictionary[player.id] = player;
            }

            return dictionary;
        }

        public static async Task< FantasyFootballBdo> GetFantasyFootball()
        {
            // TODO:Implement Exception Handling.

            FantasyFootballDto responseDto = null;

            using (var apiClient = new ApiClientJson("https://fantasy.premierleague.com/drf/bootstrap-static"))
            {
                var response = await apiClient.GetAsync<FantasyFootballDto>(null, null);

                // TODO: Error Handling.

                var fileName = $"OT Fantasy Football - {DateTime.UtcNow:yyyy-MMM-dd}.json";

                File.WriteAllText($"wwwroot\\{fileName}", response.ResponseText);

                responseDto = response.ResponseDto;
            }

            var fantasyFootballBdo = new FantasyFootballBdo()
            {
                Teams = ProcessTeams(responseDto),
                PlayerTypes = ProcessPlayerTypes(responseDto),
                Players = ProcessPlayers(responseDto)
            };

            return fantasyFootballBdo;
        }
    }
}
