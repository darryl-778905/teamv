using System;
using Microsoft.Practices.ServiceLocation;

namespace MobilePoll.Infrastructure.Ioc
{
    public interface IServiceContainer : IServiceLocator, IDisposable
    {
        IServiceContainer BeginLifetimeScope();
    }
}