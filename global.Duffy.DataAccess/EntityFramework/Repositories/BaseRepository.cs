using System;
using global.Duffy.DataAccess.EntityFramework.Databases;
using Microsoft.EntityFrameworkCore;

namespace global.Duffy.DataAccess.EntityFramework.Repositories
{
    public interface IBaseRepository<TDatabase, TEntity, TEntityIdType>
    {
        TEntity Get(TEntityIdType entityId);
    }

    public class BaseRepository<TDatabase, TEntity, TEntityIdType> : IBaseRepository<TDatabase, TEntity, TEntityIdType>
        where TDatabase : IBaseDatabase
        where TEntity : class
        where TEntityIdType : struct
    {
        private TDatabase Database { get; }

        public BaseRepository(TDatabase database)
        {
            Database = database;
        }

        public TEntity Get(TEntityIdType entityId)
        {
            return Database.Context.Set<TEntity>().Find(entityId);
        }
    }
}
