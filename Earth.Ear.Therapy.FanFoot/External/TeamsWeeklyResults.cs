using Earth.Ear.Therapy.FanFoot.BusinessDataObjects;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using Earth.Ear.Therapy.FanFoot.DataTransferObjects.PremierLeague;
using System;
using System.Collections.Generic;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using log4net;
using System.Reflection;
using Earth.Ear.Therapy.FanFoot.Extensions;
using Earth.Ear.Therapy.FanFoot.Helpers;

namespace Earth.Ear.Therapy.FanFoot.External
{
    public interface ITeamsWeeklyResults
    {
        void Create(FantasyFootballBdo fantasyFootballBdo);
    }

    public class TeamsWeeklyResults : ITeamsWeeklyResults
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private int _seasonId = -1;

        private int _weekOffset = -1;

        private FantasyFootballBdo _fantasyFootballBdo = null;

        private int _teamSequenceIndex = -1;

        private ISeasonsRepository SeasonsRepository { get; }

        private ITeamsRepository TeamsRepository { get; }

        private IPlayersRepository PlayersRepository { get; }

        public TeamsWeeklyResults(ISeasonsRepository seasonsRepository, ITeamsRepository teamsRepository, IPlayersRepository playersRepository)
        {
            SeasonsRepository = seasonsRepository;
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
            Log.Info($"AddTeamPlayer. teamEntity.TeamId = {teamEntity.TeamId}");

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

            PlayersRepository.SaveChanges();
        }

        public void Create(FantasyFootballBdo fantasyFootballBdo)
        {
            var utcNow = DateTime.UtcNow;

            var seasonEntity = SeasonsRepository.Get(utcNow);

            if (seasonEntity == null)
                throw new Exception($"There is no Season covering the Date = {utcNow:yyyy-MMM-dd}.");

            _seasonId = seasonEntity.SeasonId;

            _weekOffset = DateTimeHelper.GetWeekOffset(seasonEntity, utcNow);

            var teamEntity = TeamsRepository.GetFirstOrDefault(entity => (entity.SeasonId == _seasonId) && (entity.WeekOffset == _weekOffset));

            if (teamEntity != null)
                throw new Exception($"There already exists a Weekly Results Set covering the Date = {utcNow:yyyy-MMM-dd}, otherwise known as Week Offset = {_weekOffset}.");

            _fantasyFootballBdo = fantasyFootballBdo;

            foreach (var teamBdoPair in _fantasyFootballBdo.Teams)
            {
                AddTeam(teamBdoPair.Value);
            }
        }
    }
}
