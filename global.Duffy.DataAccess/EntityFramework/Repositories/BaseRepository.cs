using System;
using System.Collections.Generic;
using System.Linq;
using global.Duffy.DataAccess.EntityFramework.Databases;
using Microsoft.EntityFrameworkCore;

namespace global.Duffy.DataAccess.EntityFramework.Repositories
{
    public interface IBaseRepository<TDatabase, TEntity, TEntityIdType>
    {  
        void Create(TEntity entity);

        TEntity Get(TEntityIdType entityId);

        TEntity GetFirstOrDefault(Func<TEntity, bool> where);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetMany(Func<TEntity, bool> where);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        int SaveChanges();
    }

    public class BaseRepository<TDatabase, TEntity, TEntityIdType> : IBaseRepository<TDatabase, TEntity, TEntityIdType>
        where TDatabase : IBaseDatabase
        where TEntity : class
        where TEntityIdType : struct
    {
        protected TDatabase Database { get; }

        public void Create(TEntity entity)
        {
            Database.Context.Set<TEntity>().Add(entity);
        }

        public BaseRepository(TDatabase database)
        {
            Database = database;
        }

        public TEntity Get(TEntityIdType entityId)
        {
            return Database.Context.Set<TEntity>().Find(entityId);
        }

        public TEntity GetFirstOrDefault(Func<TEntity, bool> where)
        {
            return Database.Context.Set<TEntity>().FirstOrDefault(where);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Database.Context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return Database.Context.Set<TEntity>().Where(where);
        }

        public void Update(TEntity entity)
        {
            Database.Context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            Database.Context.Set<TEntity>().Remove(entity);
        }

        public int SaveChanges()
        {
            return Database.SaveChanges();
        }
    }
}
