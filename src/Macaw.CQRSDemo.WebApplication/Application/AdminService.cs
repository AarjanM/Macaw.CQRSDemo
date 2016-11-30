using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Commands;

namespace Macaw.CQRSDemo.WebApplication.Application
{
    public enum AdminAction
    {
        Unknown = 0,
        ResetDb = 1,
    }

    public class AdminService
    {
        private readonly IEventBus _eventBus;

        public AdminService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void ProcessAction(AdminAction action)
        {
            switch (action)
            {
                case AdminAction.ResetDb:
                    _eventBus.Send(new ResetDbCommand());
                    break;
            }
        }
    }
}