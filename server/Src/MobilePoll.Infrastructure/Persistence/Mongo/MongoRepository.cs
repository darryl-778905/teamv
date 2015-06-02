using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MobilePoll.Persistence;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MobilePoll.Infrastructure.Persistence.Mongo
{
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MongoCollection<TEntity> mongoCollection;

        public Expression Expression { get { return mongoCollection.AsQueryable().Expression; } }
        public Type ElementType { get { return mongoCollection.AsQueryable().ElementType; } }
        public IQueryProvider Provider { get { return mongoCollection.AsQueryable().Provider; } }

        public MongoRepository(MongoCollection<TEntity> mongoCollection)
        {
            this.mongoCollection = mongoCollection;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return mongoCollection.AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public TEntity Get(Guid id)
        {
            return mongoCollection.FindOneById(id);
        }

        public TEntity Get(int id)
        {
            return mongoCollection.FindOneById(id);
        }

        public TEntity Add(TEntity newEntity)
        {
            mongoCollection.Save(newEntity);
            return newEntity;
        }

        public TEntity Update(TEntity entity)
        {
            mongoCollection.Save(entity);
            return entity;
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}