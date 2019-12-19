using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain.Competitions;

namespace TeamGame.Domain.Playoffs
{
    public class PlayoffTeam : IDataObject, ICompetitionTeam
    {
        public long Id { get; set; }
        public Team Parent { get; set; }
        public ICompetition Competition { get; set; }
        public string Name { get; set; }
        public int Skill { get; set; }
    }
}
