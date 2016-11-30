using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Commands;
using Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Events;
using Macaw.CQRSDemo.WebApplication.Hubs;
using Microsoft.AspNetCore.SignalR.Infrastructure;

namespace Macaw.CQRSDemo.WebApplication.Handlers
{
    public class LiveScoreRefreshHandler : Handler,
        IHandleMessage<MatchStatusChangedEvent>,
        IHandleMessage<GoalScoredEvent>,
        IHandleMessage<TimeoutRequestedEvent>,
        IHandleMessage<UndoCommand>
    {
        private readonly IConnectionManager _connectionManager;

        public LiveScoreRefreshHandler(IEventStore eventStore, IConnectionManager connectionManager) : base(eventStore)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(MatchStatusChangedEvent message)
        {
            Refresh();
        }

        public void Handle(GoalScoredEvent message)
        {
            Refresh();
        }

        public void Handle(TimeoutRequestedEvent message)
        {
            Refresh();
        }

        public void Handle(UndoCommand message)
        {
            Refresh();
        }

        private void Refresh()
        {
            var hubContext = _connectionManager.GetHubContext<LiveScoreHub>();
            hubContext.Clients.All.Refresh();
        }

    }
}