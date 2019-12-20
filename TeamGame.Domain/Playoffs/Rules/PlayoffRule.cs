using System;
using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain.Competitions;

namespace TeamGame.Domain.Playoffs.Rules
{
    public class PlayoffRule
    {
        public string RuleName { get; set; }
        public string PlayoffName { get; set; }
        public IList<PlayoffGroupRule> InitialGroupRules { get; set; }
        public IList<PlayoffSeriesRule> PlayoffSeriesRules { get; set; }

        public Playoff CreatePlayoff(IList<ICompetition> ParentCompetitions, int number, int year, int startingDay, PlayoffGameCreator gameCreator)
        {
            var playoff = new Playoff() { GameCreator = gameCreator, Year = year, Number = number, Name = PlayoffName, StartingDay = startingDay };

            InitialGroupRules.ToList().ForEach(groupRule =>
            {
                groupRule.AddTeamsFromRuleToCompetition(ParentCompetitions.Where(p => p.Name.Equals(groupRule.FromCompetitionName)).First(), playoff);
            });


        }
        public PlayoffTeam CreateAndAddTeam(Season season, IList<SeasonTeamRule> rule)
        {
            var team = new SeasonTeam(season, rule[0].Parent, rule[0].Parent.Name, rule[0].Parent.Skill, 2, 1, null);

            rule.ToList().ForEach(tr =>
            {
                var div = season.Divisions.ToList().Where(sd => sd.Name.Equals(tr.Division.Name)).First();

                div.AddTeam(team);
            });

            return team;
        }
    }
}
