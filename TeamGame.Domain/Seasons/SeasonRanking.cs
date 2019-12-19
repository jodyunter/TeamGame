using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain.Competitions;
using TeamGame.Domain.Standing;

namespace TeamGame.Domain.Seasons
{
    public class SeasonRanking:IRanking
    {

        //public ICompetitionTeam Team { get; set; }
        public int Rank { get; set; }
        public SeasonDivision Division { get; set; }
        public string Group { get { return Division.Name; } }

        public ITeam Team
        {
            get { return Team; }
            set
            {
                if (value is SeasonTeam)
                {
                    Team = (SeasonTeam)value;
                }
                else
                {
                    throw new SeasonException("Can't add a non season team to a season team ranking");
                }
            }
        }

        public SeasonRanking() { }
        public SeasonRanking(SeasonTeam team, int rank, SeasonDivision division)
        {
            Team = team;
            Rank = rank;
            Division = division;
        }
        public static void UpdateDivisionRankings(SeasonDivision division)
        {
            var teams = Standings.SortTeams(division.Teams.ToList<StandingsTeam>());

            if (division.Ranking == null || division.Ranking.Count == 0)
            {
                division.Ranking = new List<SeasonRanking>();
                teams.ToList().ForEach(team =>
                {
                    division.Ranking.Add(new SeasonRanking((SeasonTeam)team, -1, division));
                });
            }

            int i = 1;

            teams.ToList().ForEach(team =>
            {
                var rank = division.Ranking.Where(rank => rank.Team.Name.Equals(team.Name)).First();
                rank.Rank = i++; //assign then increment                
            });
            
        }
    }
}
