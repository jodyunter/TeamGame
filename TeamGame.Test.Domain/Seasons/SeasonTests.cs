using System;
using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain.Seasons;
using Xunit;

namespace TeamGame.Test.Domain.Seasons
{
    public class SeasonTests
    {
        [Fact]
        public void ShouldAddDivisionToSeasonOnlyTopLevelDivision()
        {
            var season = new Season()
            {
                Number = 1,
                Year = 1,
                Name = "First Season"
            };

            var league = new SeasonDivision("League", season, DivisionLevel.League, null, null, null);

            for (int i = 0; i < 10; i++)
            {
                league.AddTeam(new SeasonTeam() { Name = "Team " + i });
            }

            season.AddDivision(league);

            Assert.StrictEqual(10, season.Teams.Count);

            season.Teams.ToList().ForEach(team =>
            {
                Assert.NotNull(team.Competition);
            });

            Assert.NotNull(league.Season);
        }

        [Fact]
        public void ShouldAddDivisionToSeasonTwoLevelDivision()
        {
            var season = new Season()
            {
                Number = 1,
                Year = 1,
                Name = "First Season"
            };

            var league = new SeasonDivision("League", null, DivisionLevel.League, null, null, null);
            var conference = new SeasonDivision("Conference", null, DivisionLevel.Conference, null, null, null);

            for (int i = 0; i < 10; i++)
            {
                conference.AddTeam(new SeasonTeam() { Name = "Team " + i });
            }

            season.AddDivision(league);

            Assert.Null(season.Teams);

            season.AddDivision(conference);

            Assert.StrictEqual(10, season.Teams.Count);
        }
    }
}
