using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using global.Duffy.DataAccess.EntityFramework.Repositories;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy
{
    public interface ITeamsRepository : IBaseRepository<IFanFootTherapyDatabase, TeamEntity, int>
    {
    }

    public class TeamsRepository : BaseRepository<IFanFootTherapyDatabase, TeamEntity, int>, ITeamsRepository
    {
        public TeamsRepository(IFanFootTherapyDatabase database) : base(database)
        {
        }
    }
}
