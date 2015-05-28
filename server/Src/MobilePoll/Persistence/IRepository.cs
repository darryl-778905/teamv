using System;
using System.Linq;

namespace MobilePoll.Persistence
{
    public interface IRepository<TEntity> : IQueryable<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);
        TEntity Get(int id);
        TEntity Add(TEntity newEntity);
        void Remove(TEntity entity);
    }
}