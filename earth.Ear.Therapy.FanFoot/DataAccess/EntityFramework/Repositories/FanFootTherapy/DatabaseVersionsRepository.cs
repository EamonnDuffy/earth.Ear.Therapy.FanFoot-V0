using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using global.Duffy.DataAccess.EntityFramework.Repositories;

namespace earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy
{
    public interface IDatabaseVersionsRepository : IBaseRepository<DatabaseVersionEntity, int>
    {
    }

    public class DatabaseVersionsRepository : BaseRepository<IFanFootTherapyDatabase, DatabaseVersionEntity, int>, IDatabaseVersionsRepository
    {
        public DatabaseVersionsRepository(IFanFootTherapyDatabase database) : base(database)
        {
        }
    }
}
