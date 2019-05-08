using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using global.Duffy.DataAccess.EntityFramework.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy
{
    public interface IPlayersRepository : IBaseRepository<IFanFootTherapyDatabase, PlayerEntity, int>
    {
        IEnumerable<PlayerEntity> Get(int teamId);
    }

    public class PlayersRepository : BaseRepository<IFanFootTherapyDatabase, PlayerEntity, int>, IPlayersRepository
    {
        public PlayersRepository(IFanFootTherapyDatabase database) : base(database)
        {
        }

        public IEnumerable<PlayerEntity> Get(int teamId)
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
