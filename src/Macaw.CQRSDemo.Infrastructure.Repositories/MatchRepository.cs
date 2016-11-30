using System.Linq;
using Macaw.CQRSDemo.Infrastructure.Repositories.DataModels;
using Macaw.CQRSDemo.Infrastructure.Repositories.Extensions;

namespace Macaw.CQRSDemo.Infrastructure.Repositories
{
    public class MatchRepository
    {
        private readonly DemoContext _demoContext;

        public MatchRepository(DemoContext demoContext)
        {
            _demoContext = demoContext;
        }

        public Match FindById(string id)
        {
            return _demoContext.Matches.FirstOrDefault(m => m.MatchId == id);
        }

        public void Save(Match match)
        {
            var existing = FindById(match.MatchId);
            if (existing == null)
            {
                _demoContext.Matches.Add(match);
            }
            else
            {
                match.CopyPropertiesTo(existing);
            }
            _demoContext.SaveChanges();
        }

        public IQueryable<Match> Find()
        {
            return (from m in _demoContext.Matches select m);
        }
    }
}