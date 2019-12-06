using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain
{
    public interface IRanking
    {
        ITeam Team { get; set; }
        int Rank { get; set; }
        string Group { get; }
    }
}
