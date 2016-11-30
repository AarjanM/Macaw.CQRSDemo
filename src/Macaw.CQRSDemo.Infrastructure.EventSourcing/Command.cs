namespace Macaw.CQRSDemo.Infrastructure.EventSourcing
{
    public abstract class Command : Message
    {
        public string Name { get; protected set; }
    }
}