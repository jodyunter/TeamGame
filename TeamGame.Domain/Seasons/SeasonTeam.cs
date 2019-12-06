using System.Collections.Generic;
using TeamGame.Domain.Standing;
using System.Linq;
using TeamGame.Domain.Competitions;

namespace TeamGame.Domain.Seasons
{
    public class SeasonTeam : StandingsTeam, IDataObject, ICompetitionTeam
    {
        public long Id { get; set; }
        public ICompetition Competition { get; set; }        
        public IList<SeasonDivision> Divisions { get; set; } //teams can exist in more than one division per season so we can group teams how we want

        public void AddDivisionToTeam(SeasonDivision division)
        {
            if (Divisions == null)
            {
                Divisions = new List<SeasonDivision>();
            }

            if (!IsDivisionInList(division))
            {
                Divisions.Add(division);
            }

        }

        public bool IsDivisionInList(SeasonDivision division)
        {
            return Divisions.ToList().Where(d => d.Name.Equals(division.Name)).FirstOrDefault() != null;
        }
     
    }
}
