using System.Collections.Generic;
using Macaw.CQRSDemo.MatchPlay.QueryStack.Models;

namespace Macaw.CQRSDemo.WebApplication.ViewModels
{
    public class LiveScoreViewModel : ViewModelBase
    {
        public LiveScoreViewModel()
        {
            LiveMatches = new List<MatchInProgress>();
        }

        public List<MatchInProgress> LiveMatches { get; }
    }
}