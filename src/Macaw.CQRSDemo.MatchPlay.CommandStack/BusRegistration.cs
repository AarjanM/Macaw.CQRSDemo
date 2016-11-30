using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.MatchPlay.CommandStack.Handlers;
using Macaw.CQRSDemo.MatchPlay.CommandStack.Sagas;
using Microsoft.Extensions.DependencyInjection;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack
{
    public static class BusRegistration
    {
        public static void RegisterMatchPlayCommandStack(this IEventBus eventBus)
        {
            eventBus.RegisterSaga<ResetDbSaga>();
            eventBus.RegisterSaga<MatchSaga>();
            eventBus.RegisterHandler<MatchProjectionHandler>();
        }

        public static void AddMatchPlayCommandStack(this IServiceCollection services)
        {
            services.AddTransient<ResetDbSaga>();
            services.AddTransient<MatchSaga>();
            services.AddTransient<MatchProjectionHandler>();
        }
    }
}