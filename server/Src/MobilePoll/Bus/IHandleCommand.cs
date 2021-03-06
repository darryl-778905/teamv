namespace MobilePoll.Bus
{
    public interface IHandleCommand<in TCommand> where TCommand : class, ICommand
    {
        void Execute(TCommand c);
    }
}