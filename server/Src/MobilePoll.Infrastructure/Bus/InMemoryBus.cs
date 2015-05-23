using System;
using System.Diagnostics;
using MobilePoll.Bus;
using MobilePoll.Infrastructure.Ioc;
using MobilePoll.Ioc;
using MobilePoll.Logging;
using MobilePoll.Persistence;

namespace MobilePoll.Infrastructure.Bus
{
    //[DebuggerNonUserCode, DebuggerStepThrough]
    internal class InMemoryBus : ILocalBus
    {
        private static readonly ILog Logger = LogFactory.BuildLogger(typeof(InMemoryBus));
        private readonly IServiceContainer serviceContainer;
        private readonly IMessageDispatcher dispatcher;

        public IUnitOfWork UnitOfWork { get; set; }

        public InMemoryBus(IServiceContainer serviceContainer, IMessageDispatcher dispatcher)
        {
            this.serviceContainer = serviceContainer;
            this.dispatcher = dispatcher;
        }

        public void Execute(ICommand command)
        {
            Logger.Info("Executing command: {0}", command.ToString());

            try
            {
                using (var lifetimeScope = serviceContainer.BeginLifetimeScope())
                {
                    ServiceLocator.Current.SetCurrentLifetimeScope(lifetimeScope);

                    dispatcher.DispatchToHandlers(command, serviceContainer);

                    if(UnitOfWork != null)
                        UnitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                if (UnitOfWork != null)
                    UnitOfWork.Rollback();
                
                Logger.Error(ex.GetFullExceptionMessage());
            }
            finally
            {
                ServiceLocator.Current.SetCurrentLifetimeScope(new DisposedProvider());
            }
        }

        public void Raise(IEvent @event)
        {
            if (!ServiceLocator.Current.HasServiceProvider())
                throw new InvalidOperationException("A event may only be raised within the context of an executing command.");

            Logger.Info("Raising : {0}", @event);

            dispatcher.DispatchToHandlers(@event, ServiceLocator.Current);
        }
    }
}
