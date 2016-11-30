using Macaw.CQRSDemo.Infrastructure.Repositories.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Macaw.CQRSDemo.Infrastructure.Repositories
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Match> Matches { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}