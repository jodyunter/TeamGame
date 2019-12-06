using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain;
using TeamGame.Domain.Standing;
using Xunit;

namespace TeamGame.Test.Domain.Standing
{
    public class StandingsTests
    {
        [Fact]
        public void ShouldSortStandingsTeams()
        {
            var teams = new List<StandingsTeam>()
            {
                //should be Team 1 (By Points), Team 3 (By Goal Difference), Team 2 (By Goal Difference), Team 4 (By Goal Difference), Team 5 (Less Wins), Team 6 (less goals)
                new StandingsTeam() {Name = "Team 1", Rank = -1, Wins = 100, Loses = 0, Ties = 0, GoalsFor = 0, GoalsAgainst = 0 },
                new StandingsTeam() {Name = "Team 2", Rank = -1, Wins = 50, Loses = 50, Ties = 50, GoalsFor = 0, GoalsAgainst = 0 },
                new StandingsTeam() {Name = "Team 3", Rank = -1, Wins = 50, Loses = 50, Ties = 50, GoalsFor = 12, GoalsAgainst = 0 },
                new StandingsTeam() {Name = "Team 4", Rank = -1, Wins = 50, Loses = 50, Ties = 50, GoalsFor = 0, GoalsAgainst = 12 },
                new StandingsTeam() {Name = "Team 5", Rank = -1, Wins = 44, Loses = 44, Ties = 62, GoalsFor = 15, GoalsAgainst = 12 },
                new StandingsTeam() {Name = "Team 6", Rank = -1, Wins = 44, Loses = 44, Ties = 62, GoalsFor = 13, GoalsAgainst = 10 },
                new StandingsTeam() {Name = "Team 7", Rank = -1, Wins = 50, Loses = 55, Ties = 100, GoalsFor = 13, GoalsAgainst = 10 }



            };

            var standings = new Standings() { Teams = teams };

            standings.ProcessRank();

            Assert.StrictEqual(1, teams[0].Rank);
            Assert.StrictEqual(3, teams[2].Rank);
            Assert.StrictEqual(6, teams[1].Rank);
            Assert.StrictEqual(7, teams[3].Rank);
            Assert.StrictEqual(4, teams[4].Rank);
            Assert.StrictEqual(5, teams[5].Rank);
            Assert.StrictEqual(2, teams[6].Rank);
        }

        [Fact]
        public void ShouldNotProcessGameBecauseBadHomeTeam()
        {
            var game = new Game() { Home = new Team(), Away = new StandingsTeam() };
            var standings = new Standings();

            game.Complete = true;

            Assert.ThrowsAny<StandingsException>(() => Standings.ProcessGame(game));

        }

        [Fact]
        public void ShouldNotProcessGameBecauseBadAwayTeam()
        {
            var game = new Game() { Away = new Team(), Home = new StandingsTeam() };
            var standings = new Standings();

            game.Complete = true;

            Assert.ThrowsAny<StandingsException>(() => Standings.ProcessGame(game));

        }

        [Fact]
        public void ShouldNotProcessGameBecauseGameNotComplete()
        {
            var game = new Game() { Away = new Team(), Home = new StandingsTeam() };
            var standings = new Standings();

            game.Complete = false;

            Assert.ThrowsAny<StandingsException>(() => Standings.ProcessGame(game));

        }

        [Fact]
        public void ShouldProcessHomeWinAwayLoss()
        {
            var standings = new Standings()
            {
                Teams = new List<StandingsTeam>()
                {
                    new StandingsTeam() {Name = "Team 1" },
                    new StandingsTeam() {Name = "team 2" }
                }
            };
            var homeTeam = standings.Teams[0];
            var awayTeam = standings.Teams[1];

            var game = new Game() { Home = homeTeam, Away = awayTeam };

            game.Complete = true;
            game.HomeScore = 6;
            game.AwayScore = 3;

            Standings.ProcessGame(game);

            Assert.StrictEqual(1, homeTeam.Wins);
            Assert.StrictEqual(0, homeTeam.Loses);
            Assert.StrictEqual(0, homeTeam.Ties);
            Assert.StrictEqual(6, homeTeam.GoalsFor);
            Assert.StrictEqual(3, homeTeam.GoalsAgainst);

            Assert.StrictEqual(0, awayTeam.Wins);
            Assert.StrictEqual(1, awayTeam.Loses);
            Assert.StrictEqual(0, awayTeam.Ties);
            Assert.StrictEqual(3, awayTeam.GoalsFor);
            Assert.StrictEqual(6, awayTeam.GoalsAgainst);

        }

        [Fact]
        public void ShouldProcessAwayWinHomeLoss()
        {
            var standings = new Standings()
            {
                Teams = new List<StandingsTeam>()
                {
                    new StandingsTeam() {Name = "Team 1" },
                    new StandingsTeam() {Name = "team 2" }
                }
            };
            var homeTeam = standings.Teams[0];
            var awayTeam = standings.Teams[1];

            var game = new Game() { Home = homeTeam, Away = awayTeam };

            game.Complete = true;
            game.HomeScore = 6;
            game.AwayScore = 12;

            Standings.ProcessGame(game);

            Assert.StrictEqual(0, homeTeam.Wins);
            Assert.StrictEqual(1, homeTeam.Loses);
            Assert.StrictEqual(0, homeTeam.Ties);
            Assert.StrictEqual(6, homeTeam.GoalsFor);
            Assert.StrictEqual(12, homeTeam.GoalsAgainst);

            Assert.StrictEqual(1, awayTeam.Wins);
            Assert.StrictEqual(0, awayTeam.Loses);
            Assert.StrictEqual(0, awayTeam.Ties);
            Assert.StrictEqual(12, awayTeam.GoalsFor);
            Assert.StrictEqual(6, awayTeam.GoalsAgainst);
        }

        [Fact]
        public void ShoulddProcessTie()
        {
            var standings = new Standings()
            {
                Teams = new List<StandingsTeam>()
                {
                    new StandingsTeam() {Name = "Team 1" },
                    new StandingsTeam() {Name = "team 2" }
                }
            };
            var homeTeam = standings.Teams[0];
            var awayTeam = standings.Teams[1];

            var game = new Game() { Home = homeTeam, Away = awayTeam };

            game.Complete = true;
            game.HomeScore = 6;
            game.AwayScore = 6;

            Standings.ProcessGame(game);

            Assert.StrictEqual(0, homeTeam.Wins);
            Assert.StrictEqual(0, homeTeam.Loses);
            Assert.StrictEqual(1, homeTeam.Ties);
            Assert.StrictEqual(6, homeTeam.GoalsFor);
            Assert.StrictEqual(6, homeTeam.GoalsAgainst);

            Assert.StrictEqual(0, awayTeam.Wins);
            Assert.StrictEqual(0, awayTeam.Loses);
            Assert.StrictEqual(1, awayTeam.Ties);
            Assert.StrictEqual(6, awayTeam.GoalsFor);
            Assert.StrictEqual(6, awayTeam.GoalsAgainst);
        }
    }
}
