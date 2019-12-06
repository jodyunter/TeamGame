using System.Collections.Generic;
using TeamGame.Domain.Standing;
using System.Linq;
using TeamGame.Domain.Competitions;

namespace TeamGame.Domain.Seasons
{
    public class SeasonTeam : StandingsTeam, ICompetitionTeam
    {
        public ICompetition Competition { get; set; }        
        public IList<SeasonDivision> Divisions { get; set; } //teams can exist in more than one division per season so we can group teams how we want

        public void AddDivisionToTeam(SeasonDivision division)
        {
            if (Divisions == null)
            {
                Divisions = new List<SeasonDivision>();
            }

            if (Divisions.ToList().Where(d => d.Name.Equals(division.Name)).FirstOrDefault() == null)
            {
                Divisions.Add(division);
            }
        }
     
    }
}
