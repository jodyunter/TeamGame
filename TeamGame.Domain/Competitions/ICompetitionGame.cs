using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Competitions
{
    public interface ICompetitionGame:IGame
    {
        ICompetition Competition { get; set; }
    }
}
