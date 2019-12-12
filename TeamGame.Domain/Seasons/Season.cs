using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain.Competitions;
using TeamGame.Domain.Scheduling;
using TeamGame.Domain.Standing;

namespace TeamGame.Domain.Seasons
{
    public class Season:ICompetition
    {
        public int Number { get; set; }
        public int Year { get; set; }
        public int StartingDay { get; set; }
        public string Name {get; set; }

        public bool Started { get; set; } = false;
        public bool Complete { get; set; } = false; //will need to work with schedule service to find unfinished games

        public IList<ICompetitionTeam> Teams { get; set; } = new List<ICompetitionTeam>();
        public IList<SeasonDivision> Divisions { get; set; } = new List<SeasonDivision>();
        public Schedule Schedule { get; set; }

        public IGameCreator GameCreator { get; set; }

        public IList<SeasonRanking> Ranking {
            get
            {
                var result = new List<SeasonRanking>();
                Divisions.ToList().ForEach(division =>
                {
                    result.AddRange(division.Ranking);
                });

                return result;
            }    
        
        }
       
        public void AddTeam(ICompetitionTeam team)
        {

            if (team is SeasonTeam)
            {
                var exists = Teams.Where(t => t.Equals(team.Name)).FirstOrDefault() != null;

                if (!exists)
                {
                    team.Competition = this;

                    Teams.Add(team);

                    var seasonTeam = (SeasonTeam)team;

                    seasonTeam.Divisions.ToList().ForEach(d =>
                    {
                        AddDivision(d);
                    });                   
                }
            }
            else
            {
                throw new SeasonException("Trying to add a non-season team to season");
            }
        }

        public void AddTeamToDivision(SeasonDivision division, SeasonTeam team)
        {
            //first if the division
            if (!team.IsDivisionInList(division))
            {
                team.AddDivisionToTeam(division);
            }

            if (!division.IsTeamInList(team))
            {
                division.AddTeam(team);
            }
        }
        
        public void AddDivision(SeasonDivision division)
        {
            division.Season = this;
                        
            if (division.GetTeamsThatBelongToDivision() != null)
            {
                division.GetTeamsThatBelongToDivision().ToList().ForEach(t =>
                {
                    if (Teams.Where(team => team.Parent.Id == t.Parent.Id).FirstOrDefault() == null)
                    {
                        AddTeam(t);
                    }
                });                
            }
            
            if (Divisions.Where(d => d.Name.Equals(division.Name)).FirstOrDefault() == null)
            {
                Divisions.Add(division);

                division.Children.ToList().ForEach(cd =>
                {
                    AddDivision(cd);
                });    
            }
            else
            {
                //throw new SeasonException("Division was already added. " + division.Name);
            }

            
        }

        
        public void UpdateRankings()
        {
            Divisions.ToList().ForEach(division =>
            {
                SeasonRanking.UpdateDivisionRankings(division);
            });
        }
        //this shouldn't be called without updating rankings first
        public Standings GetStandingsByDivision(SeasonDivision division)
        {
            //update the division rankings
            //create the standings
            //add the teams and assign ranks            
            var standings = new Standings() { Name = division.Name };
            standings.Teams = new List<StandingsTeam>();

            division.Ranking.ToList().ForEach(sr =>
            {
                sr.Team.Rank = sr.Rank;
                standings.Teams.Add(sr.Team);

            });

            return standings;
        }

        public IList<Standings> GetStandingsByDivisionLevel(DivisionLevel level)
        {
            var result = new List<Standings>();

            Divisions.Where(d => d.Level == level).ToList().ForEach(ds =>
            {
                result.Add(GetStandingsByDivision(ds));
            });

            return result;
        }
        
        public void ProcessGames(IList<ICompetitionGame> games)
        {
            games.ToList().ForEach(g =>
            {
                ProcessGame(g);
            });
        }
        public void ProcessGame(ICompetitionGame game)
        {
  
            Standings.ProcessGame((SeasonGame)game);
            
        }
    }
}
