using System;
using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain;
using TeamGame.Domain.Seasons;
using TeamGame.Domain.Seasons.Rules;

namespace TeamGame.UI.ConsoleApp.TestData
{
    public class DataSetup
    {

        public static void AddTeamsForRule(string divisionName, IList<SeasonDivisionRule> divisionRules, IList<SeasonTeamRule> fullTeamList, params Team[] teams)
        {
            var divisionRule = divisionRules.ToList().Where(d => d.Name.Equals(divisionName)).First();

            for (int i = 0; i < teams.Length; i++)
            {
                var rule = new SeasonTeamRule(teams[i], divisionRule, true);
                fullTeamList.Add(rule);
                divisionRule.AddTeam(rule);
            }            
        }
        public static Team CreateTeam(long id, string name, int skill)
        {
            return new Team(id, name, skill);
        }

        public static void AddDivisionRule(string parent, IList<SeasonDivisionRule> fullDivisionList, DivisionLevel level, params string[] divisionNames)
        {
            for (int i = 0; i < divisionNames.Length; i++)
            {
                var parentDivRule = fullDivisionList.Where(d => d.Name.Equals(parent)).FirstOrDefault();
                var divisionRule = new SeasonDivisionRule(level, divisionNames[i], parentDivRule);

                if (parentDivRule != null)
                {
                    parentDivRule.AddChildRule(divisionRule);
                }

                fullDivisionList.Add(divisionRule);
            }
        }

        public static SeasonRule SetupSeasonRule(int numberOfTeams)
        {            
            var rule = new SeasonRule();

            /*
             * var leagueRule= new SeasonDivisionRule(DivisionLevel.League, "NHL", null);
            var easternRule = new SeasonDivisionRule(DivisionLevel.Conference, "Eastern", leagueRule);
            var westernRule = new SeasonDivisionRule(DivisionLevel.Conference, "Western", leagueRule);
            var canadaRule = new SeasonDivisionRule(DivisionLevel.Country, "Canada", null);
            var bcRule = new SeasonDivisionRule(DivisionLevel.Province, "BC", canadaRule);
            var albertaRule = new SeasonDivisionRule(DivisionLevel.Province, "Alberta", canadaRule);
            var ontarioRule = new SeasonDivisionRule(DivisionLevel.Province, "Ontario", canadaRule);
            var quebecRule = new SeasonDivisionRule(DivisionLevel.Province, "Quebec", canadaRule);
            var manitobaRule = new SeasonDivision

            leagueRule.AddChildRule(easternRule);
            leagueRule.AddChildRule(westernRule);
            canadaRule.AddChildRule(bcRule);
            canadaRule.AddChildRule(albertaRule);
            canadaRule.AddChildRule(ontarioRule);
            canadaRule.AddChildRule(quebecRule);
            */
            var leagueName = "NHL";
            var easternName = "Eastern";
            var westernName = "Western";
            var canadaName = "Canada";
            var bcName = "BC";
            var albertaName = "Alberta";
            var ontarioName = "Ontario";
            var quebecName = "Quebec";
            var manitobaName = "Manitoba";
            var usaName = "USA";
            var michiganName = "Michigan";
            var notOntarioName = "Not Ontario";

            var divisionRules = new List<SeasonDivisionRule>();

            AddDivisionRule(null, divisionRules, DivisionLevel.League, leagueName);
            AddDivisionRule(leagueName, divisionRules, DivisionLevel.Conference, easternName, westernName);
            AddDivisionRule(null, divisionRules, DivisionLevel.Country, canadaName, usaName);
            AddDivisionRule(canadaName, divisionRules, DivisionLevel.Province, bcName, albertaName, ontarioName, quebecName, manitobaName);
            AddDivisionRule(usaName, divisionRules, DivisionLevel.Province, michiganName);
            AddDivisionRule(null, divisionRules, DivisionLevel.SubDivision, notOntarioName);

            long id = 0;

            var toronto = CreateTeam(id++, "Toronto", 5);
            var montreal = CreateTeam(id++, "Montreal", 5);
            var ottawa = CreateTeam(id++, "Ottawa", 5);
            var detroit = CreateTeam(id++, "Detroit", 5);
            var quebecCity = CreateTeam(id++, "Quebec City", 5);
            var hamilton = CreateTeam(id++, "Hamilton", 5);

            var vancouver = CreateTeam(id++, "Vancouver", 5);
            var calgary = CreateTeam(id++, "Calgary", 5);
            var edmonton = CreateTeam(id++, "Edmonton", 5);
            var winnipeg = CreateTeam(id++, "Winnipeg", 5);

            var teamRules = new List<SeasonTeamRule>();


            AddTeamsForRule(easternName, divisionRules, teamRules, toronto, montreal, ottawa, detroit, quebecCity, hamilton);
            AddTeamsForRule(westernName, divisionRules, teamRules, vancouver, calgary, edmonton, winnipeg);
            AddTeamsForRule(bcName, divisionRules, teamRules, vancouver);
            AddTeamsForRule(albertaName, divisionRules, teamRules, calgary, edmonton);
            AddTeamsForRule(ontarioName, divisionRules, teamRules, ottawa, toronto, hamilton);
            AddTeamsForRule(quebecName, divisionRules, teamRules, montreal, quebecCity);
            AddTeamsForRule(manitobaName, divisionRules, teamRules, winnipeg);
            AddTeamsForRule(michiganName, divisionRules, teamRules, detroit);
            AddTeamsForRule(notOntarioName, divisionRules, teamRules, detroit, quebecCity, montreal);


            var leagueScheduleRule = SeasonScheduleRule.CreateDivisionalRule("NHL Rule", divisionRules.Where(d => d.Name.Equals(leagueName)).First(), 2, true);            
            var westernScheduleRule = SeasonScheduleRule.CreateDivisionalRule("Western Rule", divisionRules.Where(d => d.Name.Equals(westernName)).First(), 3, true);
            var ontarioScheduleRule = SeasonScheduleRule.CreateDivisionalRule("Ontario Rule", divisionRules.Where(d => d.Name.Equals(ontarioName)).First(), 2, true);
            var notOntarioScheduleRule = SeasonScheduleRule.CreateDivisionalRule("Not Ontario Rule", divisionRules.Where(d => d.Name.Equals(notOntarioName)).First(), 2, true);
            var easternScheduleRule = SeasonScheduleRule.CreateDivisionalRule("Eastern Rule", divisionRules.Where(d => d.Name.Equals(easternName)).First(), 1, true);                        
            
            var scheduleRules = new List<SeasonScheduleRule>()
            {
                leagueScheduleRule,
                westernScheduleRule,
                ontarioScheduleRule,
                notOntarioScheduleRule,
                easternScheduleRule
            };

            rule.DivisionRules = divisionRules;
            rule.TeamRules = teamRules;
            rule.ScheduleRules = scheduleRules;

            return rule;
        }
    }
}
