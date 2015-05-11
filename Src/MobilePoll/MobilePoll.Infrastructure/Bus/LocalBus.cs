using System;
using MobilePoll.Infrastructure.Ioc;
using MobilePoll.Infrastructure.Logging;
using MobilePoll.Infrastructure.Persistence;

namespace MobilePoll.Infrastructure.Bus
{
    internal class LocalBus : ILocalBus
    {
        private static readonly ILogger Logger = LogFactory.BuildLogger(typeof(LocalBus));
        private readonly IServiceContainer serviceContainer;
        private readonly IMessageDispatcher dispatcher;

        public IUnitOfWork UnitOfWork { get; set; }

        public LocalBus(IServiceContainer serviceContainer, IMessageDispatcher dispatcher)
        {
            this.serviceContainer = serviceContainer;
            this.dispatcher = dispatcher;
        }

        public void Execute(ICommand command)
        {
            Logger.Info("Executing command: {0}", command.ToString());

            try
            {
                using (var scope = serviceContainer.BeginLifetimeScope())
                {
                    ServiceLocator.Current.SetCurrentLifetimeScope(scope);

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
