using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamGame.Domain.Seasons.Rules
{
    public class SeasonRule:IDataObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<SeasonDivisionRule> DivisionRules { get; set; }
        public IList<SeasonTeamRule> TeamRules { get; set; }
        public Season Create(Season previousSeason, int number, int year, int startingDay)
        {

            var season = new Season() { Name = Name, Complete = false, Number = number, Year = year, StartingDay = startingDay, Started = false };

            CreateAndAddDivision(season, DivisionRules);
            CreateAndAddTeams(season, TeamRules);

            return season;
        }

        public void CreateAndAddTeams(Season season, IList<SeasonTeamRule> TeamRules)
        {

        }

        public SeasonTeam CreateAndAddTeam(Season season, IList<SeasonTeamRule> rule)
        {
            var team = new SeasonTeam(season, rule[0].Parent, rule[0].Parent.Name, rule[0].Parent.Skill 2, 1, null);

            rule.ToList().ForEach(tr =>
            {
                var div = season.Divisions.ToList().Where(t => t.Name.Equals(tr.Division))
            });
            team.AddDivisionToTeam

         
        }
        public void CreateAndAddDivision(Season season, IList<SeasonDivisionRule> rules)
        {
            var seasonDivMasterList = new List<SeasonDivision>();

            //start the chain for each parent div
            rules.Where(r => r.Parent == null).ToList().ForEach(dr =>
            {
                CreateAndAddDivision(season, seasonDivMasterList, dr);
            });

            season.Divisions = seasonDivMasterList;
        }
        public void CreateAndAddDivision(Season season, IList<SeasonDivision> masterList, SeasonDivisionRule rule)
        {
  
            //setup division by rule
            var parent = masterList.Where(dr => dr.Name.Equals(rule.Parent.Name)).FirstOrDefault(); //must be an object or else this fails

            var newDiv = new SeasonDivision(rule.Name, season, rule.Level, parent);

            masterList.Add(newDiv);
            

            rule.Children.ToList().ForEach(cdr =>
            {
                CreateAndAddDivision(season, masterList, cdr);
            });
        }

    }
}
