using MobilePoll.Persistence;
using MongoDB.Driver;

namespace MobilePoll.Infrastructure.Persistence.Mongo
{
    public class MongoUnitOfWork : IUnitOfWork, IRepositoryFactory
    {
        private const string ConnectionString = "mongodb://localhost:27017";
        public static bool DropDatabaseOnStartup = false;
        private readonly MongoServer server;

        static MongoUnitOfWork()
        {
            if (DropDatabaseOnStartup)
            {
                MongoClient client = new MongoClient(ConnectionString);
                MongoServer server = client.GetServer();
                var mongoDatabase = server.GetDatabase("MobilePoll");
                mongoDatabase.Drop();
            }
        }
        
        private readonly MongoDatabase mongoDatabase;

        public MongoUnitOfWork()
        {
            MongoClient client = new MongoClient(ConnectionString);
            server = client.GetServer();
            mongoDatabase = server.GetDatabase("MobilePoll");
        }

        public void Commit()
        {
            server.Disconnect();
        }

        public void Rollback()
        {
            server.Disconnect();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            MongoCollection<TEntity> mongoCollection = mongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);

            return new MongoRepository<TEntity>(mongoCollection);
        }
    }
}
