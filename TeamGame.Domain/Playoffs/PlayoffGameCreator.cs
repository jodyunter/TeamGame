using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain.Competitions;

namespace TeamGame.Domain.Playoffs
{
    public class PlayoffGameCreator:BasicGameCreator
    {
        public ICompetition Competition { get; set; }
        public PlayoffGameCreator(ICompetition competition, bool canTie, int maxOverTimePeriods):base(canTie, maxOverTimePeriods)
        {
            Competition = competition;
        }
                
        public override Game CreateGame(ITeam home, ITeam away)
        {
            return new PlayoffGame()
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
