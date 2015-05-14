using System;
using Microsoft.Practices.ServiceLocation;

namespace MobilePoll.Ioc
{
    public interface IServiceContainer : IServiceLocator, IDisposable
    {
        IServiceContainer BeginLifetimeScope();
    }
}