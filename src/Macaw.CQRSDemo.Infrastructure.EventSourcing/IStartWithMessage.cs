namespace Macaw.CQRSDemo.Infrastructure.EventSourcing
{
    public interface IStartWithMessage<in T> where T : Message
    {
        void Handle(T message);
    }
}