namespace Macaw.CQRSDemo.Infrastructure.EventSourcing
{
    public abstract class Message
    {
        public string PartionKey { get; protected set; }
    }
}