using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.MatchPlay.CommandStack.DomainModels;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Events
{
    public class GoalScoredEvent : EventBase
    {
        public GoalScoredEvent(string id, TeamId teamId, int playerId)
        {
            Code = EventType.Goal;
            PartionKey = id;
            EventArgs = new GoalScoredEventEventArgs
            {
                TeamId = teamId,
                PlayerId = playerId
            };
        }

        public class GoalScoredEventEventArgs
        {
            public TeamId TeamId { get; set; }
            public int PlayerId { get; set; }
        }

    }
}