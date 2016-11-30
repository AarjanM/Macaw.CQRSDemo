namespace Macaw.CQRSDemo.MatchPlay.QueryStack.Models
{
    public class MatchListItem
    {
        public MatchListItem(string id, string team1, string team2)
        {
            Id = id;
            Team1 = team1;
            Team2 = team2;
        }

        public MatchListItem()
            : this(string.Empty, string.Empty, string.Empty)
        {
        }

        public string Id { get; internal set; }
        public string Team1 { get; internal set; }
        public string Team2 { get; internal set; }
    }
}