using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.Data.Entities;

namespace VTS.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        private readonly ApplicationDBContext _dbContext;

        public GenericRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return true;
        }

        public bool Attach(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            return true;
        }

        public bool Delete(Guid id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Set<TEntity>().Remove(entity);
            return true;
        }

        public bool DeleteRange(List<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            return true;
        }

        public IQueryable<TEntity> GetAll(string[] include = null)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking();
            if (include != null)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }

            return query;
        }

        public TEntity GetById(Guid id)
        {

            return _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.ID == id);
        }

        public bool Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return true;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string[] include = null)
        {
            var query = _dbContext.Set<TEntity>()
                .AsNoTracking();

            query = query.Where(predicate);

            if (include != null)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }

            return query;
        }

        IQueryable<TEntity> IGenericRepository<TEntity>.GetAll(string[] include)
        {
            throw new NotImplementedException();
        }


    }
}
