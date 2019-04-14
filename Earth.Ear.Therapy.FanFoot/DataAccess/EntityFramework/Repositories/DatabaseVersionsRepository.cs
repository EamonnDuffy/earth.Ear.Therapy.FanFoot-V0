using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using global.Duffy.DataAccess.EntityFramework.Repositories;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories
{
    public interface IDatabaseVersionsRepository : IBaseRepository<IFanFootTherapyDatabase, DatabaseVersionEntity, int>
    {
    }

    public class DatabaseVersionsRepository : BaseRepository<IFanFootTherapyDatabase, DatabaseVersionEntity, int>, IDatabaseVersionsRepository
    {
        public DatabaseVersionsRepository(IFanFootTherapyDatabase database) : base(database)
        {
        }
    }
}
