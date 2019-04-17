using global.Duffy.DataAccess.EntityFramework.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace global.Duffy.DataAccess.EntityFramework.Databases
{
    public interface IBaseDatabase
    {
        DbContext Context { get; }

        IDbContextTransaction Transaction { get; }

        IDbContextTransaction BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        int SaveChanges();
    }

    public class BaseDatabase : DbContext, IBaseDatabase
    {
        public DbContext Context => this;

        public IDbContextTransaction Transaction { get; private set; }

        public BaseDatabase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public IDbContextTransaction BeginTransaction()
        {
            if (Transaction != null)
                throw new DatabaseException("A Transaction has already been Begun.");

            Transaction = Database.BeginTransaction();

            return Transaction;
        }

        public void CommitTransaction()
        {
            if (Transaction == null)
                throw new DatabaseException("A Transaction has not been Begun.");

            SaveChanges();

            Transaction.Commit();
            Transaction.Dispose();
            Transaction = null;
        }

        public void RollbackTransaction()
        {
            if (Transaction == null)
                throw new DatabaseException("A Transaction has not been Begun.");

            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
