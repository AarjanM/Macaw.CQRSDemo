using Macaw.CQRSDemo.Infrastructure.EventSourcing;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Commands
{
    public class ResetDbCommand : Command
    {
        public ResetDbCommand()
        {
            Name = "ResetDb";
        }
    }
}