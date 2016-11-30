using System;
using Macaw.CQRSDemo.Infrastructure.Common;
using Macaw.CQRSDemo.Infrastructure.Common.Extensions;
using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.MatchPlay.CommandStack.DomainModels;
using Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Commands;
using Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Events;
using Macaw.CQRSDemo.MatchPlay.QueryStack;
using Macaw.CQRSDemo.WebApplication.ViewModels;

namespace Macaw.CQRSDemo.WebApplication.Application
{
    public enum RequestedAction
    {
        Unknown = 0,
        Create = 1,
        Start = 100,
        Finish = 101,
        PeriodStart = 200,
        PeriodEnd = 201,
        Goal1 = 301,
        Goal2 = 302,
        TimeoutRequest1 = 401,
        TimeoutRequest2 = 402,
        Resume = 403,
        Undo = 9999
    }

    public class MatchService
    {
        private readonly MatchQueryFacade _facade;
        private readonly IEventBus _eventBus;

        public MatchService(MatchQueryFacade facade, IEventBus eventBus)
        {
            _facade = facade;
            _eventBus = eventBus;
        }

        // Call into the read stack to read state
        public MatchViewModel GetMatchDetails(string id)
        {

            var model = new MatchViewModel { Current = _facade.FindById(id) };

            // Handle timeouts for #1
            var canCallTimeout1 = HandleTimeoutFor(model.Current.TimeoutSummary1);
            var canCallTimeout2 = HandleTimeoutFor(model.Current.TimeoutSummary2);

            switch (model.Current.State)
            {
                case MatchState.ToBePlayed:
                    model.Actions.CanStart = true;
                    break;
                case MatchState.Warmup:
                case MatchState.Interval:
                    model.Actions.CanEnd = true;
                    model.Actions.CanStartPeriod = true;
                    model.Actions.CanUndo = true;
                    break;
                case MatchState.PlayInProgress:
                    model.Actions.CanEnd = true;
                    model.Actions.CanEndPeriod = true;
                    model.Actions.CanScoreGoal = true;
                    model.Actions.CanCallTimeout1 = canCallTimeout1;
                    model.Actions.CanCallTimeout2 = canCallTimeout2;
                    model.Actions.CanUndo = true;
                    break;
                case MatchState.Timeout:
                    model.Actions.CanEnd = true;
                    model.Actions.CanResume = true;
                    model.Actions.CanUndo = true;
                    break;
            }
            return model;
        }

        // Log the event and sync up
        public void ProcessAction(string id, RequestedAction action, string team1 = null, string team2 = null)
        {
            switch (action)
            {
                case RequestedAction.Undo:
                    _eventBus.Send(new UndoCommand(id));
                    break;
                case RequestedAction.Create:
                    _eventBus.Send(new CreateMatchCommand(id, team1, team2));
                    break;
                case RequestedAction.Start:
                    _eventBus.RaiseEvent(new MatchStatusChangedEvent(id, EventType.Start));
                    break;
                case RequestedAction.Finish:
                    _eventBus.RaiseEvent(new MatchStatusChangedEvent(id, EventType.End));
                    break;
                case RequestedAction.Resume:
                    _eventBus.RaiseEvent(new MatchStatusChangedEvent(id, EventType.Resume));
                    break;
                case RequestedAction.PeriodStart:
                    _eventBus.RaiseEvent(new MatchStatusChangedEvent(id, EventType.Period));
                    break;
                case RequestedAction.PeriodEnd:
                    _eventBus.RaiseEvent(new MatchStatusChangedEvent(id, EventType.EndPeriod));
                    break;
                case RequestedAction.Goal1:
                    _eventBus.Send(new ScoreGoalCommand(id, TeamId.Home, GetRandomPlayerId()));
                    break;
                case RequestedAction.Goal2:
                    _eventBus.Send(new ScoreGoalCommand(id, TeamId.Visitors, GetRandomPlayerId()));
                    break;
                case RequestedAction.TimeoutRequest1:
                    _eventBus.Send(new RequestTimeoutCommand(id, TeamId.Home));
                    break;
                case RequestedAction.TimeoutRequest2:
                    _eventBus.Send(new RequestTimeoutCommand(id, TeamId.Visitors));
                    break;
            }
        }

        private static int GetRandomPlayerId()
        {
            var rnd = new Random();
            return rnd.Next(2, 15);
        }

        private static bool HandleTimeoutFor(string timeoutSummary)
        {
            var canCallTimeout = false;
            if (!string.IsNullOrWhiteSpace(timeoutSummary))
            {
                var tokens = timeoutSummary.Split('/');
                if (tokens.Length == 2)
                {
                    canCallTimeout = tokens[0].ToInt() < tokens[1].ToInt();
                }
            }
            return canCallTimeout;
        }
    }
}