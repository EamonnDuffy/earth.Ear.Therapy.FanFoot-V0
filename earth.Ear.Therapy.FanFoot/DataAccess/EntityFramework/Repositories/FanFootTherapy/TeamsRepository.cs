using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using global.Duffy.DataAccess.EntityFramework.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy
{
    public interface ITeamsRepository : IBaseRepository<TeamEntity, int>
    {
        IEnumerable<TeamEntity> GetAll(int seasonId, int weekOffset);
    }

    public class TeamsRepository : BaseRepository<IFanFootTherapyDatabase, TeamEntity, int>, ITeamsRepository
    {
        public TeamsRepository(IFanFootTherapyDatabase database) : base(database)
        {
        }

        public IEnumerable<TeamEntity> GetAll(int seasonId, int weekOffset)
        {
            var teamEntities = Database
                .Context
                .Set<TeamEntity>()
                .Where(entity => (entity.SeasonId == seasonId) && (entity.WeekOffset == weekOffset))
                .OrderBy(entity => entity.PremierLeagueTeamId);

            return teamEntities;
        }
    }
}
