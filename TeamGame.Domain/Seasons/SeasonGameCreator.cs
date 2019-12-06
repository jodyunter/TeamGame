using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain.Competitions;

namespace TeamGame.Domain.Seasons
{
    public class SeasonGameCreator : BasicGameCreator
    {
        public ICompetition Competition { get; set; }
        public SeasonGameCreator(ICompetition season, bool canTie, int maxOverTimePeriods):base(canTie, maxOverTimePeriods)
        {
            Competition = season;
        }

        public override Game CreateGame(ITeam home, ITeam away)
        {
            return new SeasonGame()
            {
                Home = home,
                Away = away,
                CanTie = CanTie,
                MaxOverTimePeriods = MaxOverTimePeriods,
                Competition = Competition
            };
        }
    }
}
