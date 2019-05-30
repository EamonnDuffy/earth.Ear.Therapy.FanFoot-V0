using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using global.Duffy.DataAccess.EntityFramework.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy
{
    public interface IPlayersRepository : IBaseRepository<PlayerEntity, int>
    {
        PlayerEntity GetPreviousLast(int seasonId, int premierLeagueElementId, int weekOffset);

        IEnumerable<PlayerEntity> GetAll(int teamId);
    }

    public class PlayersRepository : BaseRepository<IFanFootTherapyDatabase, PlayerEntity, int>, IPlayersRepository
    {
        public PlayersRepository(IFanFootTherapyDatabase database) : base(database)
        {
        }

        public PlayerEntity GetPreviousLast(int seasonId, int premierLeagueElementId, int weekOffset)
        {
            var playerEntity = (from player in Database.Context.Set<PlayerEntity>()
                               join team in Database.Context.Set<TeamEntity>()
                                   on player.TeamId equals team.TeamId
                               where team.SeasonId == seasonId
                                     && player.PremierLeagueElementId == premierLeagueElementId
                                     && team.WeekOffset < weekOffset
                               orderby team.WeekOffset descending
                               select player).FirstOrDefault();

            return playerEntity;
        }

        public IEnumerable<PlayerEntity> GetAll(int teamId)
        {
            var playerEntities = Database
                .Context
                .Set<PlayerEntity>()
                .Where(entity => (entity.TeamId == teamId))
                .OrderBy(entity => entity.TeamSequenceIndex);

            return playerEntities;
        }
    }
}
