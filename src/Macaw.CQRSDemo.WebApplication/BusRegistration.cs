using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.WebApplication.Handlers;

namespace Macaw.CQRSDemo.WebApplication
{
    public static class BusRegistration
    {
        public static void RegisterLiveScoreHandler(this IEventBus eventBus)
        {
            eventBus.RegisterHandler<LiveScoreRefreshHandler>();
        }
    }
}