using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Macaw.CQRSDemo.Infrastructure.Repositories
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureReposities(this IServiceCollection services)
        {
            const string connection = @"Server=(localdb)\mssqllocaldb;Database=Macaw.CQRSDemo.Database;Trusted_Connection=True;";
            services.AddDbContext<DemoContext>(options => options.UseSqlServer(connection));

            services.AddScoped<EventRepository>();
            services.AddScoped<MatchRepository>();
            services.AddScoped<AdminRepository>();
        }
    }
}