using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobilePoll.Infrastructure.Persistence
{
    [DataContract]
    [DebuggerNonUserCode, DebuggerStepThrough]
    public class InMemoryDataStore
    {
        [DataMember]
        private Dictionary<string, object> Data { get; set; }

        [DataMember]
        private Dictionary<string, int> EntityIdentities { get; set; }

        public InMemoryDataStore()
        {
            Data = new Dictionary<string, object>();
            EntityIdentities = new Dictionary<string, int>();
        }

        internal int GetNextId<TEntity>() where TEntity : class
        {
            string type = typeof(TEntity).ToString();

            if (!EntityIdentities.ContainsKey(type))
            {
                EntityIdentities.Add(type, 1);
            }
            else
            {
                EntityIdentities[type]++;
            }

            return EntityIdentities[type];
        }        

        internal bool Remove<TEntity>(TEntity oldItem) where TEntity : class
        {
            return GetEntityList<TEntity>().Remove(oldItem);
        }

        internal void Add<TEntity>(TEntity item) where TEntity : class
        {
            GetEntityList<TEntity>().Add(item);
        }

        internal int Count<TEntity>() where TEntity : class
        {
            return GetEntityList<TEntity>().Count();
        }

        internal void Clear<TEntity>() where TEntity : class
        {
            GetEntityList<TEntity>().Clear();
        }

        internal IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : class
        {
            return GetEntityList<TEntity>().AsQueryable();
        }

        internal IQueryable<TEntity> GetData<TEntity>() where TEntity : class
        {
            return GetEntityList<TEntity>().AsQueryable();
        }

        private List<TEntity> GetEntityList<TEntity>() where TEntity : class
        {
            string type = typeof(TEntity).ToString();

            if (!Data.ContainsKey(type))
            {
                var collection = new List<TEntity>();
                Data.Add(type, collection);
                return collection;
            }

            JArray array = Data[type] as JArray; ;

            if (array != null)
            {
                Data[type] = new List<TEntity>(array.Select(i => i.ToObject<TEntity>()));
            }

            return ((List<TEntity>)Data[type]);
        }
    }
}