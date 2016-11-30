using System;
using Macaw.CQRSDemo.Infrastructure.Common;
using Macaw.CQRSDemo.Infrastructure.Common.Extensions;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.DomainModels
{
    public class Match
    {
        // Id, Team1, Team2 immutable => if they change, it is another match. 
        // Consider adding a new type for these 3 properties.
        //public static Match Undefined = new Match("", "", "");

        // Constants
        private const int TotalPeriodsInMatch = 4;
        private const int MaxTimeoutsPerPeriod = 1;

        public Match()
        {
            InitializeAsDefault();
        }

        protected Match(string id, string team1, string team2)
        {
            InitializeAsDefault();
            Id = id;
            Team1 = team1;
            Team2 = team2;
            State = MatchState.ToBePlayed;
        }

        public string Id { get; }
        public string Team1 { get; }
        public string Team2 { get; }
        public int TimeoutCount1 { get; internal set; }
        public int TimeoutCount2 { get; internal set; }
        public Score CurrentScore { get; internal set; }
        public bool IsBallInPlay { get; private set; }
        public int CurrentPeriod { get; internal set; }
        public MatchState State { get; internal set; }
        public string Venue { get; set; }
        public DateTime Day { get; set; }   // deserves further thinking in relationship with Score/State

        #region Informational

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Id) &&
                   !string.IsNullOrWhiteSpace(Team1) &&
                   !string.IsNullOrWhiteSpace(Team2) &&
                   State != MatchState.Unknown;
        }
        public bool CanRequestTimeout(TeamId id)
        {
            if (id == TeamId.Home && TimeoutCount1 < MaxTimeoutsPerPeriod)
                return true;
            if (id == TeamId.Visitors && TimeoutCount2 < MaxTimeoutsPerPeriod)
                return true;
            return false;
        }
        public bool IsInProgress()
        {
            return State > MatchState.ToBePlayed && State < MatchState.Finished;
        }
        public bool IsFinished()
        {
            return State == MatchState.Finished;
        }
        public bool IsScheduled()
        {
            return State == MatchState.ToBePlayed;
        }
        public string TimeoutSummary(TeamId id)
        {
            if (id == TeamId.Unknown)
                return string.Empty;

            var count = id == TeamId.Home ? TimeoutCount1 : TimeoutCount2;
            return string.Format("{0}/{1}", count, MaxTimeoutsPerPeriod);
        }

        public override string ToString()
        {
            return IsScheduled()
                ? string.Format("{0} vs. {1}", Team1, Team2)
                : string.Format("{0} / {1}  {2}", Team1, Team2, CurrentScore);
        }
        #endregion

        #region Behavior
        /// <summary>
        /// Starts the match
        /// </summary>
        /// <returns>this</returns>
        public Match Start()
        {
            if (!IsValid())
                return this;

            State = MatchState.Warmup;
            return this;
        }

        /// <summary>
        /// Ends the match
        /// </summary>
        /// <returns></returns>
        public void Finish()
        {
            if (!IsValid())
                return;

            State = MatchState.Finished;
        }

        /// <summary>
        /// Scores a goal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Match Goal(TeamId id)
        {
            if (!IsValid())
                return this;

            if (id == TeamId.Home)
            {
                CurrentScore = new Score(CurrentScore.TotalGoals1.Increment(),
                                         CurrentScore.TotalGoals2);
            }
            if (id == TeamId.Visitors)
            {
                CurrentScore = new Score(CurrentScore.TotalGoals1,
                                         CurrentScore.TotalGoals2.Increment());
            }

            return this;
        }

        /// <summary>
        /// Starts next period
        /// </summary>
        /// <returns>this</returns>
        public Match StartPeriod()
        {
            if (!IsValid())
                return this;

            ResetTimeouts();
            CurrentPeriod = CurrentPeriod.Increment(TotalPeriodsInMatch);
            State = MatchState.PlayInProgress;
            IsBallInPlay = true;
            return this;
        }

        /// <summary>
        /// Starts next period
        /// </summary>
        /// <returns>this</returns>
        public Match EndPeriod()
        {
            if (!IsValid())
                return this;

            IsBallInPlay = false;
            State = MatchState.Interval;

            if (CurrentPeriod == TotalPeriodsInMatch)
                Finish();

            return this;
        }

        /// <summary>
        /// Tracks a timeout requested by specified team
        /// </summary>
        /// <param name="id">Team ID</param>
        /// <returns></returns>
        public Match Timeout(TeamId id)
        {
            if (!IsValid())
                return this;

            // Should we do it?
            if (!CanRequestTimeout(id))
                return this;

            switch (id)
            {
                case TeamId.Home:
                    TimeoutCount1 = TimeoutCount1.Increment(MaxTimeoutsPerPeriod);
                    break;
                case TeamId.Visitors:
                    TimeoutCount2 = TimeoutCount2.Increment(MaxTimeoutsPerPeriod);
                    break;
                case TeamId.Unknown:
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }

            IsBallInPlay = false;
            State = MatchState.Timeout;
            return this;
        }

        /// <summary>
        /// Resume match from any sort of interruption by putting the ball in play
        /// </summary>
        /// <returns></returns>
        public Match Resume()
        {
            if (!IsValid())
                return this;

            IsBallInPlay = true;
            State = MatchState.PlayInProgress;
            return this;
        }
        #endregion


        public static class Factory
        {
            public static Match CreateNewMatch(string id, string team1, string team2)
            {
                return new Match(id, team1, team2);
            }
        }

        private void InitializeAsDefault()
        {
            State = MatchState.Unknown;
            CurrentScore = new Score();
            Venue = string.Empty;
            Day = DateTime.Today;
            IsBallInPlay = false;
            CurrentPeriod = 0;
            ResetTimeouts();
        }

        private void ResetTimeouts()
        {
            TimeoutCount1 = 0;
            TimeoutCount2 = 0;
        }
    }
}