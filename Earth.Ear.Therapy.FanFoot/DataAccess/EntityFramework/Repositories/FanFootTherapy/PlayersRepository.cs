using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using global.Duffy.DataAccess.EntityFramework.Repositories;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy
{
    public interface IPlayersRepository : IBaseRepository<IFanFootTherapyDatabase, PlayerEntity, int>
    {
    }

    public class PlayersRepository : BaseRepository<IFanFootTherapyDatabase, PlayerEntity, int>, IPlayersRepository
    {
        public PlayersRepository(IFanFootTherapyDatabase database) : base(database)
        {
        }
    }
}
