using System;
using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain.Scheduling;
using TeamGame.Domain.Seasons.Scheduling;

namespace TeamGame.Domain.Seasons.Rules
{
    public class SeasonRule:IDataObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<SeasonDivisionRule> DivisionRules { get; set; }
        public IList<SeasonTeamRule> TeamRules { get; set; }
        public IList<SeasonScheduleRule> ScheduleRules { get; set; }
        public Season Create(Season previousSeason, int number, int year, int startingDay, SeasonGameCreator gameCreator)
        {

            var season = new Season() { Name = Name, Complete = false, Number = number, Year = year, StartingDay = startingDay, Started = false };

            CreateAndAddDivision(season, DivisionRules);
            CreateAndAddTeams(season, TeamRules);

            gameCreator.Competition = season;
            season.GameCreator = gameCreator;
            season.Schedule = CreateSchedule(null, season);
            return season;
        }

        public Schedule CreateSchedule(Schedule initial, Season season)
        {
            ScheduleRules.ToList().ForEach(rule =>
            {
               initial = SeasonScheduler.CreateGamesByRule(rule, season);
            });

            return initial;
        }

        public void CreateAndAddTeams(Season season, IList<SeasonTeamRule> teamRules)
        {
            if (teamRules != null)
            {
                teamRules.ToList().Select(t => t.Parent.Id).Distinct().ToList().ForEach(parentId =>
                {
                    var team = CreateAndAddTeam(season, TeamRules.ToList().Where(tr => tr.Parent.Id == parentId).ToList());
                    season.Teams.Add(team);
                });
            }
        }

        public SeasonTeam CreateAndAddTeam(Season season, IList<SeasonTeamRule> rule)
        {
            var team = new SeasonTeam(season, rule[0].Parent, rule[0].Parent.Name, rule[0].Parent.Skill, 2, 1, null);

            rule.ToList().ForEach(tr =>
            {
                var div = season.Divisions.ToList().Where(sd => sd.Name.Equals(tr.Division.Name)).First();
                
                div.AddTeam(team);
            });

            return team;         
        }
        public void CreateAndAddDivision(Season season, IList<SeasonDivisionRule> rules)
        {
            var seasonDivMasterList = new List<SeasonDivision>();

            //start the chain for each parent div
            rules.Where(r => r.Parent == null).ToList().ForEach(dr =>
            {
                CreateAndAddDivision(season, null, seasonDivMasterList, dr);
            });

            season.Divisions = seasonDivMasterList;
        }
        public SeasonDivision CreateAndAddDivision(Season season, SeasonDivision parent, IList<SeasonDivision> masterList, SeasonDivisionRule rule)
        {


            var newDiv = new SeasonDivision(rule.Name, season, rule.Level, parent);


            rule.Children.ToList().ForEach(cdr =>
            {
                newDiv.AddChildDivision(CreateAndAddDivision(season, newDiv, masterList, cdr));
            });

            masterList.Add(newDiv);

            return newDiv;
        }

    }
}
