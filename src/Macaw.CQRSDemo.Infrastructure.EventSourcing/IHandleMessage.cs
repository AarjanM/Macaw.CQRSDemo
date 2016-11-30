namespace Macaw.CQRSDemo.Infrastructure.EventSourcing
{
    public interface IHandleMessage<in T> where T : Message
    {
        void Handle(T message);
    }
}