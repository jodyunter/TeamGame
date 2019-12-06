using System;
using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain.Seasons;
using Xunit;
namespace TeamGame.Test.Domain.Seasons
{
    public class SeasonDivisionTests
    {
        [Fact]
        public void ShouldGetTeamsWhenChildrenNullAndNoTeams()
        {
            var division = new SeasonDivision();

            Assert.StrictEqual(0, division.Teams.Count);
        }

        [Fact]
        public void ShouldGetTeamsWhenChildrenNullAndHasTeams()
        {
            var division = new SeasonDivision() { Name = "Test Div" };

            division.AddTeam(new SeasonTeam() { Name = "Team 1" });
            division.AddTeam(new SeasonTeam() { Name = "Team 2" });
            division.AddTeam(new SeasonTeam() { Name = "Team 3" });
            division.AddTeam(new SeasonTeam() { Name = "Team 4" });
            division.AddTeam(new SeasonTeam() { Name = "Team 5" });
            division.AddTeam(new SeasonTeam() { Name = "Team 6" });
            division.AddTeam(new SeasonTeam() { Name = "Team 7" });
            division.AddTeam(new SeasonTeam() { Name = "Team 8" });

            Assert.StrictEqual(8, division.Teams.Count);
        }

        [Fact]
        public void ShouldGetTeamsWithChildrenAndDoesNotHaveTeams()
        {
            var division = new SeasonDivision() { Name = "Parent Div" };
            var child1 = new SeasonDivision() { Name = "Child 1" };
            var child2 = new SeasonDivision() { Name = "Child 2" };

            child1.AddTeam(new SeasonTeam() { Name = "Team 1" });
            child1.AddTeam(new SeasonTeam() { Name = "Team 2" });
            child1.AddTeam(new SeasonTeam() { Name = "Team 3" });
            child1.AddTeam(new SeasonTeam() { Name = "Team 4" });
            child1.AddTeam(new SeasonTeam() { Name = "Team 5" });
            child2.AddTeam(new SeasonTeam() { Name = "Team 6" });
            child2.AddTeam(new SeasonTeam() { Name = "Team 7" });
            child2.AddTeam(new SeasonTeam() { Name = "Team 8" });


            child1.AddChildDivision(child2);
            division.AddChildDivision(child1);

            Assert.StrictEqual(8, division.Teams.Count);
            Assert.StrictEqual(8, child1.Teams.Count);
            Assert.StrictEqual(3, child2.Teams.Count);
        }
    }
}
