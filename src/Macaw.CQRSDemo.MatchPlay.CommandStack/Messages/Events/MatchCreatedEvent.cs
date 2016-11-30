using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.MatchPlay.CommandStack.DomainModels;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Events
{
    public class MatchCreatedEvent : EventBase
    {
        public MatchCreatedEvent(Match match)
        {
            Code = EventType.Created;
            NewMatch = match;
            PartionKey = match.Id;

            EventArgs = new MatchCreatedEventArgs
            {
                Team1 = match.Team1,
                Team2 = match.Team2
            };
        }

        public class MatchCreatedEventArgs
        {
            public string Team1 { get; set; }
            public string Team2 { get; set; }
        }

        public Match NewMatch { get; }
    }
}