using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using Earth.Ear.Therapy.FanFoot.DataTransferObjects.PremierLeague;
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

        public DbSet<SeasonEntity> Seasons { get; set; }

        public DbSet<TeamEntity> Teams { get; set; }

        public DbSet<PlayerEntity> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public FanFootTherapyDatabase(DbContextOptions<FanFootTherapyDatabase> options) : base(options)
        {
        }
    }
}
