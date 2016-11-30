using Macaw.CQRSDemo.Infrastructure.EventSourcing;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Commands
{
    public class CreateMatchCommand : Command
    {
        public string MatchId { get; }
        public string Team1 { get; }
        public string Team2 { get; }

        public CreateMatchCommand(string matchId, string team1, string team2)
        {
            MatchId = matchId;
            Team1 = team1;
            Team2 = team2;
            Name = "CreateMatch";

            // Command class has PartionKey property used to find which saga the message relates to. 
            // (Simpler schema than more general ConfigureHowToFindSaga method in NServiceBus)
            PartionKey = matchId;
        }
    }
}