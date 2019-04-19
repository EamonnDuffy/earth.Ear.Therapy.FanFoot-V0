using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using global.Duffy.DataAccess.EntityFramework.Repositories;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy
{
    public interface ISeasonsRepository : IBaseRepository<IFanFootTherapyDatabase, SeasonEntity, int>
    {
    }

    public class SeasonsRepository : BaseRepository<IFanFootTherapyDatabase, SeasonEntity, int>, ISeasonsRepository
    {
        public SeasonsRepository(IFanFootTherapyDatabase database) : base(database)
        {
        }
    }
}
