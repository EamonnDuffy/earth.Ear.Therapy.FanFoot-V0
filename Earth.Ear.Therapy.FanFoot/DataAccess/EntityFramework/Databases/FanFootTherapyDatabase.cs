using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using global.Duffy.DataAccess.EntityFramework.Databases;
using Microsoft.EntityFrameworkCore;

namespace Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases
{
    public interface IFanFootTherapyDatabase : IBaseDatabase
    {
    }

    public class FanFootTherapyDatabase : BaseDatabase, IFanFootTherapyDatabase
    {
        public DbSet<DatabaseVersionEntity> DatabaseVersions { get; set; }

        public FanFootTherapyDatabase(DbContextOptions<FanFootTherapyDatabase> options) : base(options)
        {
        }
    }
}
