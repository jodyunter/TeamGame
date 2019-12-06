using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain;
using TeamGame.Domain.Seasons;
using TeamGame.Domain.Seasons.Rules;
using Xunit;
namespace TeamGame.Test.Domain.Seasons
{
    public class SeasonDivisionRuleTests
    {
  
        public static void SetupRules(IList<SeasonDivisionRule> divisionRules, IList<SeasonTeamRule> teamRules, IList<SeasonScheduleRule> scheduleRules)
        {
            var result = new List<SeasonDivisionRule>();

            var league = new SeasonDivisionRule() { Level = DivisionLevel.League, Name = "League" };
            var westernConference = new SeasonDivisionRule { Level = DivisionLevel.Conference, Name = "Western Conference" };
            var easternConference = new SeasonDivisionRule { Level = DivisionLevel.Conference, Name = "Eastern Conference" };
            var central = new SeasonDivisionRule { Level = DivisionLevel.Division, Name = "Central" };
            var pacific = new SeasonDivisionRule { Level = DivisionLevel.Division, Name = "Pacific" };
            var atlantic = new SeasonDivisionRule { Level = DivisionLevel.Division, Name = "Altantic" };
            var south = new SeasonDivisionRule { Level = DivisionLevel.Division, Name = "South" };

        }

        [Fact]
        public void ShouldAddChildRule()
        {
            var league = new SeasonDivisionRule() { Level = DivisionLevel.League, Name = "League" };
            var westernConference = new SeasonDivisionRule { Level = DivisionLevel.Conference, Name = "Western Conference" };
            var easternConference = new SeasonDivisionRule { Level = DivisionLevel.Conference, Name = "Eastern Conference" };

            league.AddChildRule(westernConference);
            league.AddChildRule(easternConference);

            Assert.Equal("League", westernConference.Parent.Name);
            Assert.Equal("League", easternConference.Parent.Name);
            Assert.StrictEqual(2, league.Children.Count);
        }

        [Fact]
        public void ShouldAddTeam()
        {
            var league = new SeasonDivisionRule() { Level = DivisionLevel.League, Name = "League" };
            var team = new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 1", Id = 12345, Skill = 1 } };

            league.AddTeam(team);

            Assert.Equal("League", team.Division.Name);
            Assert.StrictEqual(1, league.Teams.Count);
        }

        [Fact]
        public void ShouldGetTeamsInDivisionTopLevel()
        {
            var league = new SeasonDivisionRule() { Level = DivisionLevel.League, Name = "League" };
            league.AddTeam(new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 1", Id = 12345, Skill = 1 } });
            league.AddTeam(new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 2", Id = 12345, Skill = 1 } });
            league.AddTeam(new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 3", Id = 12345, Skill = 1 } });
            league.AddTeam(new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 4", Id = 12345, Skill = 1 } });
            league.AddTeam(new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 5", Id = 12345, Skill = 1 } });
            league.AddTeam(new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 6", Id = 12345, Skill = 1 } });

            Assert.StrictEqual(6, league.Teams.Count);
        }

        [Fact]
        public void ShouldGetTeamsDivisionThreeLevels()
        {
            var league = new SeasonDivisionRule() { Level = DivisionLevel.League, Name = "League" };
            var easternConference = new SeasonDivisionRule { Level = DivisionLevel.Conference, Name = "Eastern Conference" };
            var central = new SeasonDivisionRule { Level = DivisionLevel.Division, Name = "Central" };

            league.AddChildRule(easternConference);
            easternConference.AddChildRule(central);

            league.AddTeam(new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 1", Id = 12345, Skill = 1 } });
            easternConference.AddTeam(new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 2", Id = 12345, Skill = 1 } });
            easternConference.AddTeam(new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 3", Id = 12345, Skill = 1 } });
            central.AddTeam(new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 4", Id = 12345, Skill = 1 } });
            central.AddTeam(new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 5", Id = 12345, Skill = 1 } });
            central.AddTeam(new SeasonTeamRule() { Active = true, Division = null, Parent = new Team() { Name = "Team 6", Id = 12345, Skill = 1 } });

            Assert.StrictEqual(3, central.GetTeamsInDivision().Count);
            Assert.StrictEqual(5, easternConference.GetTeamsInDivision().Count);
            Assert.StrictEqual(6, league.GetTeamsInDivision().Count);

        }
    }
}
