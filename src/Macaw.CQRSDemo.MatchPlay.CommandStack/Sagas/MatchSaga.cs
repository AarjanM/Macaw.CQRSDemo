using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.Infrastructure.Repositories;
using Macaw.CQRSDemo.MatchPlay.CommandStack.DomainModels;
using Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Commands;
using Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Events;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.Sagas
{
    public class MatchSaga : Saga,
        IStartWithMessage<CreateMatchCommand>,
        IHandleMessage<ScoreGoalCommand>,
        IHandleMessage<RequestTimeoutCommand>,
        IHandleMessage<UndoCommand>
    {
        private readonly EventRepository _eventRepository;

        public MatchSaga(IEventBus eventBus, IEventStore eventStore, EventRepository eventRepository) : base(eventBus, eventStore)
        {
            _eventRepository = eventRepository;
        }

        public void Handle(CreateMatchCommand message)
        {
            // Build the aggregate and publish the event on the bus 
            var match = Match.Factory.CreateNewMatch(message.MatchId, message.Team1, message.Team2);
            EventBus.RaiseEvent(new MatchCreatedEvent(match));
        }

        public void Handle(ScoreGoalCommand message)
        {
            // Nothing special to do, just publish the event on the bus
            EventBus.RaiseEvent(new GoalScoredEvent(message.MatchId, message.TeamId, message.PlayerId));
        }

        public void Handle(RequestTimeoutCommand message)
        {
            // Nothing special to do, just publish the event on the bus
            EventBus.RaiseEvent(new TimeoutRequestedEvent(message.MatchId, message.TeamId));
        }

        public void Handle(UndoCommand message)
        {
            _eventRepository.RemoveMostRecent(message.MatchId);
        }
    }
}