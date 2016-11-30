using Macaw.CQRSDemo.WebApplication.Application;
using Macaw.CQRSDemo.WebApplication.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Macaw.CQRSDemo.WebApplication
{
    public static class DependencyInjection
    {
        public static void AddWebApplication(this IServiceCollection services)
        {
            services.AddScoped<HomeService>();
            services.AddScoped<AdminService>();
            services.AddScoped<MatchService>();
            services.AddScoped<LiveScoreService>();
            services.AddTransient<LiveScoreRefreshHandler>();
        }
    }
}