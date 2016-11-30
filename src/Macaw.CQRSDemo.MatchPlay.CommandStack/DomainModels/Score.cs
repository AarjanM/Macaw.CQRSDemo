namespace Macaw.CQRSDemo.MatchPlay.CommandStack.DomainModels
{
    public class Score
    {
        private const int MaxGoals = 99;

        public Score(int goals1 = 0, int goals2 = 0)
        {
            TotalGoals1 = goals1;
            TotalGoals2 = goals2;
        }

        public int TotalGoals1 { get; }
        public int TotalGoals2 { get; }

        #region Informational
        public override string ToString()
        {
            return string.Format("{0} - {1}", TotalGoals1, TotalGoals2);
        }
        public bool IsLeading(TeamId id)
        {
            switch (id)
            {
                case TeamId.Home:
                    return TotalGoals1 > TotalGoals2;
                case TeamId.Visitors:
                    return TotalGoals2 > TotalGoals1;
            }
            return false;
        }
        #endregion

        public override bool Equals(object obj)
        {
            var otherScore = obj as Score;
            if (otherScore == null)
                return false;

            return otherScore.TotalGoals1 == TotalGoals1 &&
                otherScore.TotalGoals2 == TotalGoals2;
        }
        public override int GetHashCode()
        {
            return (TotalGoals2 ^ TotalGoals2).GetHashCode();
        }
    }
}