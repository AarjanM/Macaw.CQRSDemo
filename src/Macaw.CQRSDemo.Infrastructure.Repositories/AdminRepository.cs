namespace Macaw.CQRSDemo.Infrastructure.Repositories
{
    public class AdminRepository
    {
        private readonly DemoContext _demoContext;

        public AdminRepository(DemoContext demoContext)
        {
            _demoContext = demoContext;
        }

        public void ResetDb()
        {
            // Empty both DBs
            foreach (var match in _demoContext.Matches)
            {
                _demoContext.Matches.Remove(match);
            }
            foreach (var @event in _demoContext.Events)
            {
                _demoContext.Events.Remove(@event);
            }
            _demoContext.SaveChanges();
        }
    }
}