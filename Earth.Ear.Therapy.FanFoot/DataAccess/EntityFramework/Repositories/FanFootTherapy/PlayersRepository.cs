using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using global.Duffy.DataAccess.EntityFramework.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy
{
    public interface IPlayersRepository : IBaseRepository<PlayerEntity, int>
    {
        PlayerEntity GetMostRecent(int seasonId, int premierLeagueElementId);

        IEnumerable<PlayerEntity> GetAll(int teamId);
    }

    public class PlayersRepository : BaseRepository<IFanFootTherapyDatabase, PlayerEntity, int>, IPlayersRepository
    {
        public PlayersRepository(IFanFootTherapyDatabase database) : base(database)
        {
        }

        public PlayerEntity GetMostRecent(int seasonId, int premierLeagueElementId)
        {
#if false
            var playerEntity = Database
                .Context
                .Set<PlayerEntity>()
                .Where(entity => (entity.SeasonId == seasonId) && (entity.)
#else
            var playerEntity = (from player in Database.Context.Set<PlayerEntity>()
                               join team in Database.Context.Set<TeamEntity>()
                                   on player.TeamId equals team.TeamId
                               where team.SeasonId == seasonId && player.PremierLeagueElementId == premierLeagueElementId
                               orderby team.WeekOffset descending
                               select player).ToList().FirstOrDefault();
#endif

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
