using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace VTS.Data.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {

        IQueryable<TEntity> GetAll(string[] include = null);

        TEntity GetById(Guid id);

        bool Create(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(Guid id);

        bool DeleteRange(List<TEntity> entities);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string[] include = null);

        bool Attach(TEntity entity);
    }
}
