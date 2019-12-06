using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain.Competitions;

namespace TeamGame.Domain.Seasons
{
    //this may be redundant to competition game
    public class SeasonGame : Game, ICompetitionGame
    {
        public ICompetition Competition { get; set; }
    }
}
