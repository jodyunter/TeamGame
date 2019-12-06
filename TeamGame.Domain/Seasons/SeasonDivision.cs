using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamGame.Domain.Seasons
{
    public class SeasonDivision
    {
        public string Name { get; set; }
        public Season Season { get; set; }
        public DivisionLevel Level { get; set; }
        public SeasonDivision Parent { get; set; }
        public IList<SeasonDivision> Children { get; set; } = new List<SeasonDivision>();
        public IList<SeasonRanking> Ranking { get; set; } = new List<SeasonRanking>();

        public IList<SeasonTeam> _Teams = new List<SeasonTeam>();

        //this returns any teams belonging to this division OR to a child division
        public IList<SeasonTeam> Teams
        {
            get
            {
                var result = new List<SeasonTeam>();
                
                result.AddRange(_Teams);                

                Children.ToList().ForEach(child =>
                {
                    result.AddRange(child.Teams);
                });

                return result;
            }
        }

        public SeasonDivision() { }

        public SeasonDivision(string name, Season season, DivisionLevel level, SeasonDivision parent, IList<SeasonDivision> children, IList<SeasonRanking> ranking)
        {
            Name = name;
            Season = season;
            Level = level;
            Parent = parent;
            if (parent != null)
            {
                Parent.AddChildDivision(this);
            }
            Children = children;
            Ranking = ranking;            
        }

        //does the team belong to this division and not a child division?
        public bool IsTeamInList(SeasonTeam team)
        {
            return _Teams.Where(t => t.Parent.Id == team.Parent.Id).FirstOrDefault() != null;
        }
        //this could be child division
        public bool IsTeamInDivision(SeasonTeam team)
        {
            return Teams.Where(t => t.Parent.Id == team.Parent.Id).FirstOrDefault() != null;
        }

        //we only want teams actually in this division
        public IList<SeasonTeam> GetTeamsThatBelongToDivision()
        {
            return _Teams;
        }
        public void AddTeam(SeasonTeam team)
        {
   
            if (!(Children.ToList().Where(d => d.IsTeamInDivision(team)).ToList().FirstOrDefault() == null))
            {
                throw new SeasonException("Team : " + team.Name + " already belongs to a child division of " + Name);    
            }            

            if (Parent != null && Parent.IsTeamInDivision(team))
            {
                throw new SeasonException("Team : " + team.Name + " belongs to the parent division - " + Parent.Name + " - " + Name);
            }

            team.AddDivisionToTeam(this);

            _Teams.Add(team);            
        }

        public void AddChildDivision(SeasonDivision child)
        {
            Children = new List<SeasonDivision>();            

            Children.Add(child);
            child.Parent = this;
        }
    }

    
    public enum DivisionLevel
    {
        League = 0,
        Conference = 1,
        Division = 2,
        SubDivision = 3,
        Region = 4,
        SubRegion = 5,
        Province = 6,
        Country = 7,
        City = 8,
        County = 9,
        Rivalry = 10
    }
   
}
