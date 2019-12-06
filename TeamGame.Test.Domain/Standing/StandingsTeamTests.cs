using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain.Standing;
using Xunit;

namespace TeamGame.Test.Domain.Standing
{
    public class StandingsTeamTests
    {
        [Fact]
        public void ShouldProcessWin()
        {
            var team = new StandingsTeam();

            team.ProcessWin(5, 3);

            Assert.StrictEqual(5, team.GoalsFor);
            Assert.StrictEqual(3, team.GoalsAgainst);
            Assert.StrictEqual(1, team.Wins);
            Assert.StrictEqual(0, team.Loses);
            Assert.StrictEqual(0, team.Ties);

            team.ProcessWin(2, 1);
            
            Assert.StrictEqual(7, team.GoalsFor);
            Assert.StrictEqual(4, team.GoalsAgainst);
            Assert.StrictEqual(2, team.Wins);
            Assert.StrictEqual(0, team.Loses);
            Assert.StrictEqual(0, team.Ties);
        }

        [Fact]
        public void ShouldProcessTie()
        {
            var team = new StandingsTeam();

            team.ProcessTie(5, 5);

            Assert.StrictEqual(5, team.GoalsFor);
            Assert.StrictEqual(5, team.GoalsAgainst);
            Assert.StrictEqual(0, team.Wins);
            Assert.StrictEqual(0, team.Loses);
            Assert.StrictEqual(1, team.Ties);

            team.ProcessTie(2, 2);

            Assert.StrictEqual(7, team.GoalsFor);
            Assert.StrictEqual(7, team.GoalsAgainst);
            Assert.StrictEqual(0, team.Wins);
            Assert.StrictEqual(0, team.Loses);
            Assert.StrictEqual(2, team.Ties);
        }

        [Fact]
        public void ShouldProcessLoss()
        {
            var team = new StandingsTeam();

            team.ProcessLoss(5, 6);

            Assert.StrictEqual(5, team.GoalsFor);
            Assert.StrictEqual(6, team.GoalsAgainst);
            Assert.StrictEqual(0, team.Wins);
            Assert.StrictEqual(1, team.Loses);
            Assert.StrictEqual(0, team.Ties);

            team.ProcessLoss(1, 3);

            Assert.StrictEqual(6, team.GoalsFor);
            Assert.StrictEqual(9, team.GoalsAgainst);
            Assert.StrictEqual(0, team.Wins);
            Assert.StrictEqual(2, team.Loses);
            Assert.StrictEqual(0, team.Ties);
        }

        [Fact]
        public void ShouldGetGoalDifference()
        {
            var team = new StandingsTeam() { GoalsFor = 25, GoalsAgainst = 10 };

            Assert.StrictEqual(15, team.GoalDifference);
        }

        [Fact]
        public void ShouldGetPoints()
        {
            var team = new StandingsTeam() {Wins = 3, Ties = 5, Loses = 2, GoalsFor = 25, GoalsAgainst = 10 };

            Assert.StrictEqual(11, team.Points);
        }

        [Fact]
        public void ShouldGetGamesPlayed()
        {
            var team = new StandingsTeam() { Wins = 3, Ties = 5, Loses = 2, GoalsFor = 25, GoalsAgainst = 10 };

            Assert.StrictEqual(10, team.GamesPlayed);
        }
    }
}
