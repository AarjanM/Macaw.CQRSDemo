using Macaw.CQRSDemo.MatchPlay.QueryStack;
using Macaw.CQRSDemo.WebApplication.ViewModels;

namespace Macaw.CQRSDemo.WebApplication.Application
{
    public class HomeService
    {
        private readonly MatchQueryFacade _facade;

        public HomeService(MatchQueryFacade facade)
        {
            _facade = facade;
        }

        public ScheduledMatchesViewModel GetSchedulesMatches()
        {
            return new ScheduledMatchesViewModel { ScheduledMatches = _facade.FindScheduled() };
        }
    }
}