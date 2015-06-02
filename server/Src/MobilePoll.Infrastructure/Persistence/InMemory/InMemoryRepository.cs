using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MobilePoll.Logging;
using MobilePoll.Persistence;

namespace MobilePoll.Infrastructure.Persistence.InMemory
{
    [DebuggerNonUserCode, DebuggerStepThrough]
    public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ILog logger;

        private readonly InMemoryDataStore dataStore;
        private readonly PropertyInfo identityPropertyInfo;

        public Expression Expression { get { return dataStore.GetData<TEntity>().Expression; } }
        public Type ElementType { get { return dataStore.GetData<TEntity>().ElementType; } }
        public IQueryProvider Provider { get { return dataStore.GetData<TEntity>().Provider; } }

        public InMemoryRepository(InMemoryDataStore dataStore)
        {
            logger = LogFactory.BuildLogger(GetType());
            this.dataStore = dataStore;
            identityPropertyInfo = GetIdentityPropertyInformation();
        }

        private PropertyInfo GetIdentityPropertyInformation()
        {
            try
            {
                return typeof (TEntity)
                    .GetProperties()
                    .Single(propertyInfo => propertyInfo.Name.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
            }
            catch (InvalidOperationException)
            {
                return typeof (TEntity)
                    .GetProperties()
                    .Single(propertyInfo => Attribute.IsDefined(propertyInfo, typeof (KeyAttribute)));
            }
        }

        public TEntity Get(Guid id)
        {
            logger.Debug("Fetcing a {0} entity with Id {1}", typeof(TEntity).Name, id);

            return dataStore
                .AsQueryable<TEntity>()
                .SingleOrDefault(WithMatchingId(id));
        }

        public TEntity Get(int id)
        {
            logger.Debug("Fetcing a {0} entity with Id {1}", typeof(TEntity).Name, id);

            return dataStore
                .AsQueryable<TEntity>()
                .SingleOrDefault(WithMatchingId(id));
        }

        private Func<TEntity, bool> WithMatchingId(dynamic id)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
            Expression property = Expression.Property(parameter, identityPropertyInfo.Name);
            Expression target = GetConstantExpression(id);
            Expression equalsMethod = Expression.Equal(property, target);
            Func<TEntity, bool> predicate = Expression.Lambda<Func<TEntity, bool>>(equalsMethod, parameter).Compile();

            return predicate;
        }

        private static dynamic GetConstantExpression(Guid id)
        {
            return Expression.Constant(id);
        }

        private static dynamic GetConstantExpression(int id)
        {
            return Expression.Constant(id);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return dataStore.AsQueryable<TEntity>();
        }

        public TEntity Add(TEntity item)
        {
            logger.Debug("Adding a {0} entity", typeof(TEntity).Name);

            object entityId = identityPropertyInfo.GetValue(item);            
            
            if (entityId is Guid)
            {
                if (((Guid)entityId) == Guid.Empty)
                {
                    identityPropertyInfo.SetValue(item, Guid.NewGuid());
                }
            }
            else
            {
                identityPropertyInfo.SetValue(item, dataStore.GetNextId<TEntity>());
            }

            dataStore.Add(item);         

            return item;
        }

        public TEntity Update(TEntity entity)
        {
            // In memory does not do an update
            return entity;

        }

        public void Remove(TEntity item)
        {
            logger.Debug("Removing a {0} entity", typeof(TEntity).Name);

            dataStore.Remove(item);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return dataStore.GetData<TEntity>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}