using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain
{
    public class BasicGameCreator : IGameCreator
    {
        public bool CanTie { get; set; }
        public int MaxOverTimePeriods { get; set; }
        public BasicGameCreator(bool canTie, int maxOverTimePeriods)
        {
            CanTie = canTie;
            MaxOverTimePeriods = maxOverTimePeriods;
        }
        public virtual Game CreateGame(ITeam home, ITeam away)
        {
            if (home.Name == away.Name) throw new Exception("Teams can't play themselves");
            return new Game()
            {
                Home = home,
                Away = away,
                CanTie = CanTie,
                MaxOverTimePeriods = MaxOverTimePeriods
            };
        }
    }
}
