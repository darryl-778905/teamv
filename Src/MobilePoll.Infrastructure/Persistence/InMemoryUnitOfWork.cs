using MobilePoll.Infrastructure.Serialization;
using MobilePoll.Persistence;

namespace MobilePoll.Infrastructure.Persistence
{
    /// <summary>
    /// Normally a real unit of work would track changes to our persistence store and handle actual transactions. 
    /// </summary>
    //[DebuggerNonUserCode, DebuggerStepThrough]
    internal sealed class InMemoryUnitOfWork : IUnitOfWork, IRepositoryFactory
    {
        private static readonly JsonObjectSerializer Serializer;
        public static byte[] CommittedData;
        private InMemoryDataStore workingSet;

        static InMemoryUnitOfWork()
        {
            Serializer = new JsonObjectSerializer();
            CommittedData = Serializer.ToByteArray(new InMemoryDataStore());
        }

        public InMemoryUnitOfWork()
        {
            workingSet = new InMemoryDataStore();
            Commit();
        }

        public void Commit()
        {
            //if we were working with a database, this is where the transaction would be created and all changes persisted.
            CommittedData = Serializer.ToByteArray(workingSet);
        }

        public void Rollback()
        {
            workingSet = Serializer.FromByteArray<InMemoryDataStore>(CommittedData);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new InMemoryRepository<TEntity>(workingSet);
        }
    }
}