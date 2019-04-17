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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=..\\Database\\FanFoot.Therapy.V000.db");

            base.OnConfiguring(optionsBuilder);
        }

        public FanFootTherapyDatabase(DbContextOptions<FanFootTherapyDatabase> options) : base(options)
        {
        }
    }
}
