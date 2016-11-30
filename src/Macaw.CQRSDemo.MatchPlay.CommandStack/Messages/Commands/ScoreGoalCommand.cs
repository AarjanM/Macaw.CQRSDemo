using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.MatchPlay.CommandStack.DomainModels;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Commands
{
    public class ScoreGoalCommand : Command
    {
        public ScoreGoalCommand(string id, TeamId teamId, int playerId)
        {
            Name = "Goal";
            MatchId = id;
            TeamId = teamId;
            PlayerId = playerId;

            // Command class has PartionKey property used to find which saga the message relates to. 
            // (Simpler schema than more general ConfigureHowToFindSaga method in NServiceBus)
            PartionKey = id;
        }

        public string MatchId { get; private set; }
        public TeamId TeamId { get; private set; }
        public int PlayerId { get; private set; }
    }
}