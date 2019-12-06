using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Competitions
{
    public interface ICompetitionTeam:ITeam
    {
        Team Parent { get; set; }
        ICompetition Competition { get; set; }
    }
}
