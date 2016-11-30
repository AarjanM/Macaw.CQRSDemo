using System.Collections.Generic;
using Macaw.CQRSDemo.MatchPlay.QueryStack.Models;

namespace Macaw.CQRSDemo.WebApplication.ViewModels
{
    public class ScheduledMatchesViewModel : ViewModelBase
    {
        public IList<MatchListItem> ScheduledMatches { get; set; }
    }
}