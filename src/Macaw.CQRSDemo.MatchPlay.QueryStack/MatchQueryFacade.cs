using System.Collections.Generic;
using System.Linq;
using Macaw.CQRSDemo.Infrastructure.Common;
using Macaw.CQRSDemo.Infrastructure.Repositories;
using Macaw.CQRSDemo.Infrastructure.Repositories.DataModels;
using Macaw.CQRSDemo.MatchPlay.QueryStack.Models;

namespace Macaw.CQRSDemo.MatchPlay.QueryStack
{
    public class MatchQueryFacade
    {
        private readonly MatchRepository _matchRepository;

        public MatchQueryFacade(MatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        // Runs a plain query against the relational list of scheduled matches.
        /*
         * Should be noted we ideally need a relational table with only three columns. 
         * In alternative, adding a CREATE event for match which should include a 
         * DATE of play to make query of scheduled matches reliable. It depends :)
         * 
         * Here I'm using the SAME db as previous examples also to feed the LIVE module
         * effectively.
         */
        public IList<MatchListItem> FindScheduled()
        {
            return (_matchRepository.Find().Where(m => m.State == (int)MatchState.ToBePlayed)
                .Select(m => new MatchListItem()
                {
                    Id = m.MatchId,
                    Team1 = m.Team1,
                    Team2 = m.Team2
                })).ToList();
        }

        public IList<MatchInProgress> FindInProgress()
        {
            const int codeInProgressFrom = (int)MatchState.ToBePlayed;
            const int codeInProgressTo = (int)MatchState.Finished;

            var matches = _matchRepository.Find().Where(m => m.State > codeInProgressFrom && m.State < codeInProgressTo).ToList();

            return matches.Select(CopyToMatchInProgress).ToList();
        }

        public MatchInProgress FindById(string id)
        {
            var match = _matchRepository.FindById(id);
            return CopyToMatchInProgress(match);
        }

        private static MatchInProgress CopyToMatchInProgress(Match match)
        {
            return new MatchInProgress
            {
                Id = match.MatchId,
                State = (MatchState)match.State,
                Team1 = match.Team1,
                Team2 = match.Team2,
                Goal1 = match.Score1 < 0 ? "" : match.Score1.ToString(),
                Goal2 = match.Score2 < 0 ? "" : match.Score2.ToString(),
                Period = match.Period <= 0 ? "" : match.Period.ToString(),
                TimeoutSummary1 = match.Timeouts1,
                TimeoutSummary2 = match.Timeouts2
            };
        }
    }
}