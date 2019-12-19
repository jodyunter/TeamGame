using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain.Competitions;

namespace TeamGame.Domain.Playoffs
{
    public class PlayoffGame : Game, ICompetitionGame
    {
        public ICompetition Competition { get; set; }
        public PlayoffSeries Series { get; set; }
    }
}
