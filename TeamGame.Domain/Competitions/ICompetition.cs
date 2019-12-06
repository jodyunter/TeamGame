using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Competitions
{
    public interface ICompetition
    {
        IList<ICompetitionTeam> Teams { get; set; }        
        int Number { get; set; }
        int Year { get; set; }
        string Name { get; set; }
        bool Started { get; set; }
        bool Complete { get; set; }
        void ProcessGame(ICompetitionGame game);
        void AddTeam(ICompetitionTeam team);
        
    }
}
