using Macaw.CQRSDemo.Infrastructure.EventSourcing;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Events
{
    public class MatchStatusChangedEvent : EventBase
    {
        public MatchStatusChangedEvent(string id, EventType code)
        {
            Code = code;
            PartionKey = id;
        }
    }
}