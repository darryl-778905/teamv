using System.Diagnostics;
using MobilePoll.Infrastructure.Serialization;
using MobilePoll.Logging;
using MobilePoll.Persistence;

namespace MobilePoll.Infrastructure.Persistence.InMemory
{
    /// <summary>
    /// Normally a real unit of work would track changes to our persistence store and handle actual transactions. 
    /// </summary>
    [DebuggerNonUserCode, DebuggerStepThrough]
    public class InMemoryUnitOfWork : IUnitOfWork, IRepositoryFactory
    {
        private static readonly ILog Logger = LogFactory.BuildLogger(typeof(InMemoryUnitOfWork));
        private static readonly JsonObjectSerializer Serializer;
        
        public static string CommittedData { get; private set; }
        public static InMemoryDataStore WorkingSet { get; private set; }

        static InMemoryUnitOfWork()
        {
            Serializer = new JsonObjectSerializer();
            WorkingSet = new InMemoryDataStore();
            CommittedData = Serializer.Serialize(WorkingSet);
        }

        public void Commit()
        {
            Logger.Debug("Committing unit-of-work");

            //if we were working with a database, this is where the transaction would be created and all changes persisted.
            CommittedData = Serializer.Serialize(WorkingSet);
        }

        public void Rollback()
        {
            Logger.Warn("Rolling back unit-of-work");

            WorkingSet = Serializer.Deserialize<InMemoryDataStore>(CommittedData);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new InMemoryRepository<TEntity>(WorkingSet);
        }
    }
}