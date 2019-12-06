using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Competitions
{
    public interface ICompetitionTeam:ITeam
    {
        ICompetition Competition { get; set; }
    }
}
