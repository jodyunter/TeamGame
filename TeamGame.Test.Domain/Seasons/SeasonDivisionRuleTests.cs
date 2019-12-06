using System;
using System.Collections.Generic;
using System.Text;
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

        public void ShouldAddChildRule()
        {
            var league = new SeasonDivisionRule() { Level = DivisionLevel.League, Name = "League" };
            var westernConference = new SeasonDivisionRule { Level = DivisionLevel.Conference, Name = "Western Conference" };
            var easternConference = new SeasonDivisionRule { Level = DivisionLevel.Conference, Name = "Eastern Conference" };

            league.AddChildRule(westernConference);
            league.AddChildRule(easternConference);

            Assert.Equal("League Rule", westernConference.Parent.Name);
            Assert.Equal("League Rule", easternConference.Parent.Name);
            Assert.StrictEqual(2, league.Children.Count);
        }
    }
}
