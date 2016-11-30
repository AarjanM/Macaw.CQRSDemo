using Microsoft.Extensions.DependencyInjection;

namespace Macaw.CQRSDemo.MatchPlay.QueryStack
{
    public static class DependencyInjection
    {
        public static void AddMatchQueryStack(this IServiceCollection services)
        {
            services.AddScoped<MatchQueryFacade>();
        }
    }
}