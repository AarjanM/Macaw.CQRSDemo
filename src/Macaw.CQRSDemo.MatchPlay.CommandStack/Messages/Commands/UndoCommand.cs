using Macaw.CQRSDemo.Infrastructure.EventSourcing;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Commands
{
    public class UndoCommand : Command
    {
        public UndoCommand(string id)
        {
            Name = "Undo";
            MatchId = id;

            // Command class has PartionKey property used to find which saga the message relates to. 
            // (Simpler schema than more general ConfigureHowToFindSaga method in NServiceBus)
            PartionKey = id;
        }

        public string MatchId { get; private set; }
    }
}