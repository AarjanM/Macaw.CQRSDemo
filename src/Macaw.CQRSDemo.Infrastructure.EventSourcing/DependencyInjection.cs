using Microsoft.Extensions.DependencyInjection;

namespace Macaw.CQRSDemo.Infrastructure.EventSourcing
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureEventSourcing(this IServiceCollection services)
        {
            services.AddSingleton<IEventBus, InMemoryEventBus>();
            services.AddScoped<IEventStore, SqlEventStore>();
        }
    }
}