namespace MobilePoll.Infrastructure.Persistence
{
    public interface IUnitOfWork 
    {
        void Commit();
        void Rollback();
    }
}