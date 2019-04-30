﻿using Earth.Ear.Therapy.FanFoot.BusinessDataObjects;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using Earth.Ear.Therapy.FanFoot.DataTransferObjects.PremierLeague;
using System;
using System.Collections.Generic;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;

namespace Earth.Ear.Therapy.FanFoot.External
{
    public interface ITeamsWeeklyResults
    {
        void Create(int seasonId, FantasyFootballBdo fantasyFootballBdo);
    }

    public class TeamsWeeklyResults : ITeamsWeeklyResults
    {
        private int _seasonId = -1;

        private int _weekOffset = -1;

        private FantasyFootballBdo _fantasyFootballBdo = null;

        private int _teamSequenceIndex = -1;

        private ITeamsRepository TeamsRepository { get; }

        private IPlayersRepository PlayersRepository { get; }

        public TeamsWeeklyResults(ITeamsRepository teamsRepository, IPlayersRepository playersRepository)
        {
            TeamsRepository = teamsRepository;
            PlayersRepository = playersRepository;
        }

        private Dictionary<int, Element> GetSpecificTeamPlayers(ElementType playerType, Dictionary<int, Element> teamPlayers)
        {
            var specificTeamPlayers = new Dictionary<int, Element>();

            // TODO: Figure out how to use LINQ to do this.

            foreach (var pair in teamPlayers)
            {
                if (pair.Value.element_type == playerType.id)
                {
                    specificTeamPlayers.Add(pair.Key, pair.Value);
                }
            }

            return specificTeamPlayers;
        }

        private Dictionary<int, Element> GetTeamPlayers(Team teamBdo)
        {
            var teamPlayers = new Dictionary<int, Element>();

            // TODO: Figure out how to use LINQ to do this.

            foreach (var pair in _fantasyFootballBdo.Players)
            {
                if (pair.Value.team == teamBdo.id)
                {
                    teamPlayers.Add(pair.Key, pair.Value);
                }
            }

            return teamPlayers;
        }

        private void AddTeamPlayer(TeamEntity teamEntity, ElementType playerType, Dictionary<int, Element> players)
        {
            var specificTeamPlayerTypes = GetSpecificTeamPlayers(playerType, players);

            foreach (var pair in specificTeamPlayerTypes)
            {
#if DEBUG
                var elementTypeId = pair.Value.element_type;
#endif

                var playerEntity = new PlayerEntity()
                {
                    TeamId = teamEntity.TeamId,
                    TeamSequenceIndex = _teamSequenceIndex++,
                    PlayerTypeNameSingular = playerType.singular_name,
                    PlayerTypeNamePlural = playerType.plural_name,
                    PremierLeagueElementId = pair.Value.id,
                    PremierLeagueElementTypeId = playerType.id, // TODO: Should be same as pair.Value.element_type.
                    FirstName = pair.Value.first_name,
                    SecondName = pair.Value.second_name,
                    NowCost = pair.Value.now_cost,
                    TotalPoints = pair.Value.total_points,
                    Status = pair.Value.status,
                    News = pair.Value.news,
                    CreatedDateTimeUtc = DateTime.UtcNow
                };

                // TODO: Exceptions.

                PlayersRepository.Create(playerEntity);
                PlayersRepository.SaveChanges();
            }
        }

        private void AddTeam(Team teamBdo)
        {
            var teamEntity = new TeamEntity()
            {
                SeasonId = _seasonId,
                WeekOffset = _weekOffset,
                PremierLeagueTeamId = teamBdo.id,
                TeamName = teamBdo.name,
                CreatedDateTimeUtc = DateTime.UtcNow
            };

            // TODO: Exception handling.

            TeamsRepository.Create(teamEntity);
            TeamsRepository.SaveChanges();

            var teamPlayers = GetTeamPlayers(teamBdo);

            _teamSequenceIndex = 0;

            foreach (var playerTypePair in _fantasyFootballBdo.PlayerTypes)
            {
                AddTeamPlayer(teamEntity, playerTypePair.Value, teamPlayers);
            }
        }

        public void Create(int seasonId, FantasyFootballBdo fantasyFootballBdo)
        {
            _fantasyFootballBdo = fantasyFootballBdo;

            foreach (var teamBdoPair in _fantasyFootballBdo.Teams)
            {
                AddTeam(teamBdoPair.Value);
            }
        }
    }
}
