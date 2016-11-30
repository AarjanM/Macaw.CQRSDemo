namespace Macaw.CQRSDemo.Infrastructure.EventSourcing
{
    public interface IEventBus
    {
        void Send<T>(T command) where T : Command;

        void RaiseEvent<T>(T @event) where T : EventBase;

        void RegisterSaga<T>() where T : Saga;

        void RegisterHandler<T>();
    }
}