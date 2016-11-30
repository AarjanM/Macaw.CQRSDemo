using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.MatchPlay.CommandStack.DomainModels;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Commands
{
    public class RequestTimeoutCommand : Command
    {
        public RequestTimeoutCommand(string id, TeamId teamId)
        {
            Name = "RequestTimeout";
            MatchId = id;
            TeamId = teamId;

            // Command class has PartionKey property used to find which saga the message relates to. 
            // (Simpler schema than more general ConfigureHowToFindSaga method in NServiceBus)
            PartionKey = id;
        }

        public string MatchId { get; private set; }
        public TeamId TeamId { get; private set; }
    }
}