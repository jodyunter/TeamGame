using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain;
using TeamGame.Domain.Seasons;
using TeamGame.Domain.Seasons.Rules;
using Xunit;
using System.Linq;

namespace TeamGame.Test.Domain.Seasons.Rules
{
    public class SeasonRuleTests
    {
        [Fact]
        public void ShouldCreateSeason()
        {
            var seasonRule = new SeasonRule() { Name = "Season Testing" };

            var league = new SeasonDivisionRule() { Name = "League", Level = DivisionLevel.League };
            var conf1 = new SeasonDivisionRule() { Name = "Conference 1", Level = DivisionLevel.Conference };
            var conf2 = new SeasonDivisionRule() { Name = "Conference 2", Level = DivisionLevel.Conference };
            var div1 = new SeasonDivisionRule() { Name = "Div 1", Level = DivisionLevel.Division };
            var div2 = new SeasonDivisionRule() { Name = "Div 2", Level = DivisionLevel.Division };
            var div3 = new SeasonDivisionRule() { Name = "Div 3", Level = DivisionLevel.Division };
            var div4 = new SeasonDivisionRule() { Name = "Div 4", Level = DivisionLevel.Division };

            var oddDivParent = new SeasonDivisionRule() { Name = "Odd Div", Level = DivisionLevel.Region };
            var oddDivChild = new SeasonDivisionRule() { Name = "Odd Div Child", Level = DivisionLevel.City };

            var teamRule1 = new SeasonTeamRule() { Parent = new Team() { Id = 1, Name = "Team 1" }, Active = true, Division = div1 };
            var teamRule2 = new SeasonTeamRule() { Parent = new Team() { Id = 2, Name = "Team 2" }, Active = true, Division = div1 };
            var teamRule3 = new SeasonTeamRule() { Parent = new Team() { Id = 3, Name = "Team 3" }, Active = true, Division = div1 };
            var teamRule4 = new SeasonTeamRule() { Parent = new Team() { Id = 4, Name = "Team 4" }, Active = true, Division = div2 };
            var teamRule5 = new SeasonTeamRule() { Parent = new Team() { Id = 5, Name = "Team 5" }, Active = true, Division = div2 };
            var teamRule6 = new SeasonTeamRule() { Parent = new Team() { Id = 6, Name = "Team 6" }, Active = true, Division = div2 };
            var teamRule7 = new SeasonTeamRule() { Parent = new Team() { Id = 7, Name = "Team 7" }, Active = true, Division = div3 };
            var teamRule8 = new SeasonTeamRule() { Parent = new Team() { Id = 8, Name = "Team 8" }, Active = true, Division = div3 };
            var teamRule9 = new SeasonTeamRule() { Parent = new Team() { Id = 9, Name = "Team 9" }, Active = true, Division = div3 };
            var teamRule10 = new SeasonTeamRule() { Parent = new Team() { Id = 10, Name = "Team 10" }, Active = true, Division = div4 };
            var teamRule11 = new SeasonTeamRule() { Parent = new Team() { Id = 11, Name = "Team 11" }, Active = true, Division = div4 };
            var teamRule12 = new SeasonTeamRule() { Parent = new Team() { Id = 12, Name = "Team 12" }, Active = true, Division = div4 };

            var teamRule13 = new SeasonTeamRule() { Parent = teamRule6.Parent, Active = true, Division = oddDivChild };
            var teamRule14 = new SeasonTeamRule() { Parent = teamRule12.Parent, Active = true, Division = oddDivChild };

            league.AddChildRule(conf1);
            league.AddChildRule(conf2);
            conf1.AddChildRule(div1);
            conf1.AddChildRule(div2);
            conf2.AddChildRule(div3);
            conf2.AddChildRule(div4);
            oddDivParent.AddChildRule(oddDivChild);

            seasonRule.DivisionRules = new List<SeasonDivisionRule>() { league, conf1, conf2, div1, div2, div3, div4, oddDivParent, oddDivChild };
            seasonRule.TeamRules = new List<SeasonTeamRule>() { teamRule1, teamRule2, teamRule3, teamRule4, teamRule5, teamRule6, teamRule7, teamRule8, teamRule9, teamRule10, teamRule11, teamRule12, teamRule13, teamRule14 };
            var season = seasonRule.Create(null, 1, 1, 1);

            Assert.StrictEqual(9, season.Divisions.Count);
            Assert.StrictEqual(12, season.Teams.Count);

            Assert.StrictEqual(2, season.Divisions.ToList().Where(d => d.Name.Equals("League")).First().Children.Count);
            Assert.StrictEqual(2, season.Divisions.ToList().Where(d => d.Name.Equals("Conference 1")).First().Children.Count);
            Assert.StrictEqual(2, season.Divisions.ToList().Where(d => d.Name.Equals("Conference 1")).First().Children.Count);
            Assert.StrictEqual(0, season.Divisions.ToList().Where(d => d.Name.Equals("Div 1")).First().Children.Count);
            Assert.StrictEqual(0, season.Divisions.ToList().Where(d => d.Name.Equals("Div 2")).First().Children.Count);
            Assert.StrictEqual(0, season.Divisions.ToList().Where(d => d.Name.Equals("Div 3")).First().Children.Count);
            Assert.StrictEqual(0, season.Divisions.ToList().Where(d => d.Name.Equals("Div 4")).First().Children.Count);
            Assert.StrictEqual(1, season.Divisions.ToList().Where(d => d.Name.Equals("Odd Div")).First().Children.Count);
            Assert.StrictEqual(0, season.Divisions.ToList().Where(d => d.Name.Equals("Odd Div Child")).First().Children.Count);

            Assert.StrictEqual(0, season.Divisions.ToList().Where(d => d.Name.Equals("League")).First()._Teams.Count);
            Assert.StrictEqual(0, season.Divisions.ToList().Where(d => d.Name.Equals("Conference 1")).First()._Teams.Count);
            Assert.StrictEqual(0, season.Divisions.ToList().Where(d => d.Name.Equals("Conference 1")).First()._Teams.Count);
            Assert.StrictEqual(3, season.Divisions.ToList().Where(d => d.Name.Equals("Div 1")).First()._Teams.Count);
            Assert.StrictEqual(3, season.Divisions.ToList().Where(d => d.Name.Equals("Div 2")).First()._Teams.Count);
            Assert.StrictEqual(3, season.Divisions.ToList().Where(d => d.Name.Equals("Div 3")).First()._Teams.Count);
            Assert.StrictEqual(3, season.Divisions.ToList().Where(d => d.Name.Equals("Div 4")).First()._Teams.Count);
            Assert.StrictEqual(0, season.Divisions.ToList().Where(d => d.Name.Equals("Odd Div")).First()._Teams.Count);
            Assert.StrictEqual(2, season.Divisions.ToList().Where(d => d.Name.Equals("Odd Div Child")).First()._Teams.Count);

            Assert.StrictEqual(12, season.Divisions.ToList().Where(d => d.Name.Equals("League")).First().Teams.Count);
            Assert.StrictEqual(6, season.Divisions.ToList().Where(d => d.Name.Equals("Conference 1")).First().Teams.Count);
            Assert.StrictEqual(6, season.Divisions.ToList().Where(d => d.Name.Equals("Conference 1")).First().Teams.Count);
            Assert.StrictEqual(3, season.Divisions.ToList().Where(d => d.Name.Equals("Div 1")).First().Teams.Count);
            Assert.StrictEqual(3, season.Divisions.ToList().Where(d => d.Name.Equals("Div 2")).First().Teams.Count);
            Assert.StrictEqual(3, season.Divisions.ToList().Where(d => d.Name.Equals("Div 3")).First().Teams.Count);
            Assert.StrictEqual(3, season.Divisions.ToList().Where(d => d.Name.Equals("Div 4")).First().Teams.Count);

            Assert.StrictEqual(2, season.Divisions.ToList().Where(d => d.Name.Equals("Odd Div")).First().Teams.Count);
            Assert.StrictEqual(2, season.Divisions.ToList().Where(d => d.Name.Equals("Odd Div Child")).First().Teams.Count);
        }
    }
}
