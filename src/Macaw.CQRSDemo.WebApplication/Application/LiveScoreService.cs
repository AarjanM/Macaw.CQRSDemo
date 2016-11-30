using Macaw.CQRSDemo.MatchPlay.QueryStack;
using Macaw.CQRSDemo.WebApplication.ViewModels;

namespace Macaw.CQRSDemo.WebApplication.Application
{
    public class LiveScoreService
    {

            private readonly MatchQueryFacade _facade;

            public LiveScoreService(MatchQueryFacade facade)
            {
                _facade = facade;
            }

            public LiveScoreViewModel GetLiveViewModel()
            {
                var model = new LiveScoreViewModel();
                model.LiveMatches.AddRange(_facade.FindInProgress());

                return model;
            }

    }
}