using System;
using System.Collections.Generic;
using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.Infrastructure.Repositories;
using Macaw.CQRSDemo.Infrastructure.Repositories.DataModels;
using Macaw.CQRSDemo.MatchPlay.CommandStack.DomainModels;
using Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Commands;
using Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Events;
using Newtonsoft.Json;
using Match = Macaw.CQRSDemo.MatchPlay.CommandStack.DomainModels.Match;
using MatchData = Macaw.CQRSDemo.Infrastructure.Repositories.DataModels.Match;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.Handlers
{
    public class MatchProjectionHandler: Handler,
        IHandleMessage<MatchCreatedEvent>,
        IHandleMessage<MatchStatusChangedEvent>,
        IHandleMessage<GoalScoredEvent>,
        IHandleMessage<TimeoutRequestedEvent>,
        IHandleMessage<UndoCommand>
    {
        private readonly MatchRepository _matchRepository;

        public MatchProjectionHandler(MatchRepository matchRepository, IEventStore eventStore) : base(eventStore)
        {
            _matchRepository = matchRepository;
        }

        public void Handle(MatchCreatedEvent message)
        {
            var freshlyCreatedMatch = message.NewMatch;
            if (freshlyCreatedMatch == null)
                return;

            // Adapt from domain model to persistence model and save
            var persistableMatch = CopyFrom(freshlyCreatedMatch);
            _matchRepository.Save(persistableMatch);
        }

        public void Handle(MatchStatusChangedEvent message)
        {
            UpdateSnapshot(message.PartionKey);
        }

        public void Handle(GoalScoredEvent message)
        {
            UpdateSnapshot(message.PartionKey);
        }

        public void Handle(TimeoutRequestedEvent message)
        {
            UpdateSnapshot(message.PartionKey);
        }

        public void Handle(UndoCommand message)
        {
            UpdateSnapshot(message.PartionKey);
        }

        private void UpdateSnapshot(string matchId)
        {
            // 1. get the match by replaying events
            // 2. extract the status data to denormalize
            var allEvents = EventStore.All(matchId);
            var replayedMatch = Replay(allEvents);
            if (replayedMatch == null)
                return;

            // Adapt from domain model to persistence model and save
            var persistableMatch = CopyFrom(replayedMatch);
            _matchRepository.Save(persistableMatch);
        }

        public Match Replay(IEnumerable<Event> events)
        {
            var match = new Match();
            foreach (var e in events)
            {
                var whatHappened = (EventType)Enum.Parse(typeof(EventType), e.EventType);
                switch (whatHappened)
                {
                    case EventType.Created:
                        var matchCreatedEventArgs = JsonConvert.DeserializeObject<MatchCreatedEvent.MatchCreatedEventArgs>(e.Payload);
                        match = Match.Factory.CreateNewMatch(e.PartitionKey, matchCreatedEventArgs.Team1, matchCreatedEventArgs.Team2);
                        break;
                    case EventType.Start:
                        match.Start();
                        break;
                    case EventType.End:
                        match.Finish();
                        break;
                    case EventType.Period:
                        match.StartPeriod();
                        break;
                    case EventType.EndPeriod:
                        match.EndPeriod();
                        break;
                    case EventType.Goal:
                        var goalScoredEventEventArgs = JsonConvert.DeserializeObject<GoalScoredEvent.GoalScoredEventEventArgs>(e.Payload);
                        match.Goal(goalScoredEventEventArgs?.TeamId ?? TeamId.Unknown);
                        break;
                    case EventType.Timeout:
                        var timeoutRequestedEventArgs = JsonConvert.DeserializeObject<TimeoutRequestedEvent.TimeoutRequestedEventArgs>(e.Payload);
                        match.Timeout(timeoutRequestedEventArgs?.TeamId ?? TeamId.Unknown);
                        break;
                    case EventType.Resume:
                        match.Resume();
                        break;
                }
            }
            return match;
        }

        private static MatchData CopyFrom(Match match)
        {
            var persistedMatch = new MatchData()
            {
                MatchId = match.Id,
                Period = match.CurrentPeriod,
                Score1 = match.CurrentScore.TotalGoals1,
                Score2 = match.CurrentScore.TotalGoals2,
                State = (int)match.State,
                Team1 = match.Team1,
                Team2 = match.Team2,
                Timeouts1 = match.TimeoutSummary(TeamId.Home),
                Timeouts2 = match.TimeoutSummary(TeamId.Visitors)
            };
            return persistedMatch;
        }
    }
}