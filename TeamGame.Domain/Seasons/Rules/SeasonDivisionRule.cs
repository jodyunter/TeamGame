using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamGame.Domain.Seasons.Rules
{
    public class SeasonDivisionRule
    {
        public DivisionLevel Level { get; set; }
        public string Name { get; set; }
        public SeasonDivisionRule Parent { get; set; }
        public IList<SeasonDivisionRule> Children { get; set; } = new List<SeasonDivisionRule>();
        public IList<SeasonTeamRule> Teams { get; set; } = new List<SeasonTeamRule>();

        public IList<Team> GetTeamsInDivision()
        {
            var result = new List<Team>();

            result.AddRange(Teams.Select(t => t.Parent));

            Children.ToList().ForEach(childRules =>
            {
                result.AddRange(childRules.GetTeamsInDivision());
            });

            return result;
        }

        public void AddChildRule(SeasonDivisionRule rule) 
        {
            rule.Parent = this;
            Children.Add(rule);
        }

        public void AddTeam(SeasonTeamRule team)
        {
            team.Division = this;
            Teams.Add(team);
        }
    }
}
