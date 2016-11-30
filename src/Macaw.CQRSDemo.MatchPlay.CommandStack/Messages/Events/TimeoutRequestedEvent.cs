using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.MatchPlay.CommandStack.DomainModels;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Events
{
    public class TimeoutRequestedEvent : EventBase
    {
        public TimeoutRequestedEvent(string id, TeamId teamId)
        {
            Code = EventType.Timeout;
            PartionKey = id;
            EventArgs = new TimeoutRequestedEventArgs
            {
                TeamId = teamId
            };
        }

        public class TimeoutRequestedEventArgs
        {
            public TeamId TeamId { get; set; }
        }
    }
}