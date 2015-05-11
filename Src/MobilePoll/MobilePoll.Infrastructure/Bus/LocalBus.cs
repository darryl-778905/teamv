using System;
using MobilePoll.Infrastructure.Ioc;
using MobilePoll.Infrastructure.Logging;

namespace MobilePoll.Infrastructure
{
    public class LocalBus : ILocalBus
    {
        private static readonly ILogger Logger = LogFactory.BuildLogger(typeof(LocalBus));
        private readonly IServiceContainer serviceContainer;
        private readonly IMessageDispatcher dispatcher;
        private readonly IUnitOfWork unitOfWork;

        public LocalBus(IServiceContainer serviceContainer, IMessageDispatcher dispatcher, IUnitOfWork unitOfWork)
        {
            this.serviceContainer = serviceContainer;
            this.dispatcher = dispatcher;
            this.unitOfWork = unitOfWork;
        }

        public void Execute(ICommand command)
        {
            Logger.Info("Executing command: {0}", command.ToString());

            try
            {
                using (var scope = serviceContainer.BeginLifetimeScope())
                {
                    ServiceLocator.Current.SetCurrentLifetimeScope(scope);


                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();;
                Logger.Error(ex.GetFullExceptionMessage());
            }
            finally
            {
                ServiceLocator.Current.SetCurrentLifetimeScope(null);
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
