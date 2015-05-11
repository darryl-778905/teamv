using System;
using MobilePoll.Infrastructure.Persistence;

namespace MobilePoll.Infrastructure.TestShell.Stubs
{
    public class UnitOfWorkStub : IUnitOfWork
    {
        public void Commit()
        {
            Console.WriteLine("Committing unit-of-work");
        }

        public void Rollback()
        {
            Console.WriteLine("Rolling back unit-of-work");
        }
    }
}