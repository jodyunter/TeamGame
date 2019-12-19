using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain.Playoffs;
using Xunit;
namespace TeamGame.Test.Domain.Playoffs
{
    public class PlayoffSeriesTests
    {
        [Theory]
        //0-0
        [InlineData(0, 0, 0, 4, 4)]
        [InlineData(0, 0, 1, 4, 3)]
        [InlineData(0, 0, 2, 4, 2)]
        [InlineData(0, 0, 3, 4, 1)]
        [InlineData(0, 0, 4, 4, 0)]
        //1-0
        [InlineData(1, 0, 0, 4, 3)]
        [InlineData(1, 0, 1, 4, 2)]
        [InlineData(1, 0, 2, 4, 1)]
        [InlineData(1, 0, 3, 4, 0)]
        //0-1
        [InlineData(0, 1, 0, 4, 3)]
        [InlineData(0, 1, 1, 4, 2)]
        [InlineData(0, 1, 2, 4, 1)]
        [InlineData(0, 1, 3, 4, 0)]
        //2-0
        [InlineData(2, 0, 0, 4, 2)]
        [InlineData(2, 0, 1, 4, 1)]
        [InlineData(2, 0, 2, 4, 0)]
        //0-2
        [InlineData(0, 2, 0, 4, 2)]
        [InlineData(0, 2, 1, 4, 1)]
        [InlineData(0, 2, 2, 4, 0)]
        //3-0
        [InlineData(3, 0, 0, 4, 1)]
        [InlineData(3, 0, 1, 4, 0)]
        //0-3
        [InlineData(0, 3, 0, 4, 1)]
        [InlineData(0, 3, 1, 4, 0)]
        //4-0
        [InlineData(4, 0, 0, 4, 0)]
        //0-4
        [InlineData(0, 4, 0, 4, 0)]
        //1-1
        [InlineData(1, 1, 0, 4, 3)]
        [InlineData(1, 1, 1, 4, 2)]
        [InlineData(1, 1, 2, 4, 1)]
        [InlineData(1, 1, 3, 4, 0)]
        //2-1
        [InlineData(2, 1, 0, 4, 2)]
        [InlineData(2, 1, 1, 4, 1)]
        [InlineData(2, 1, 2, 4, 0)]
        //1-2
        [InlineData(1, 2, 0, 4, 2)]
        [InlineData(1, 2, 1, 4, 1)]
        [InlineData(1, 2, 2, 4, 0)]
        //3-1
        [InlineData(3, 1, 0, 4, 1)]
        [InlineData(3, 1, 1, 4, 0)]
        //1-3
        [InlineData(1, 3, 0, 4, 1)]
        [InlineData(1, 3, 1, 4, 0)]
        //4-1
        [InlineData(4, 1, 0, 4, 0)]
        //1-4
        [InlineData(1, 4, 0, 4, 0)]
        public void ShouldTestCreateGames(int startingHomeWins, int startingAwayWins, int unfinishedGames, int requiredWins, int expectedGamesCreated)
        {
            var series = new PlayoffSeries()
            {
                HomeWins = startingHomeWins,
                AwayWins = startingAwayWins,
                RequiredWins = requiredWins
            };

            for (int i = 0; i < startingHomeWins; i++)
            {
                series.Games.Add(new PlayoffGame() { Complete = true, Processed = true, HomeScore = 1, AwayScore = 0 });
            }

            for (int i = 0; i < startingAwayWins; i++)
            {
                series.Games.Add(new PlayoffGame() { Complete = true, Processed = true, HomeScore = 1, AwayScore = 0 });
            }

            for (int i = 0; i < unfinishedGames; i++)
            {
                series.Games.Add(new PlayoffGame() { Complete = false, Processed = false, HomeScore = 0, AwayScore = 0 });
            }

            var newGames = series.CreateRequiredGames(new PlayoffGameCreator(new Playoff(), false, 5));

            Assert.StrictEqual(expectedGamesCreated, newGames.Count);
        }

        [Theory]
        [InlineData(0, 0, 2, false)]
        [InlineData(1, 0, 2, false)]
        [InlineData(1, 1, 2, false)]
        [InlineData(2, 0, 2, true)]
        [InlineData(0, 2, 2, true)]
        [InlineData(2, 1, 2, true)]
        [InlineData(1, 2, 2, true)]
        public void ShouldBeComplete(int homeWins, int awayWins, int requiredWins, bool expected)
        {
            var series = new PlayoffSeries()
            {
                HomeWins = homeWins,
                AwayWins = awayWins,
                RequiredWins = requiredWins
            };

            Assert.Equal(expected, series.Complete);
        }

        [Fact]
        public void ShouldProcessHomeWin()
        {
            var homeTeam = new PlayoffTeam() { Id = 15 };
            var awayTeam = new PlayoffTeam() { Id = 12 };

            var game = new PlayoffGame() { Complete = true, Home = homeTeam, Away = awayTeam, HomeScore = 12, AwayScore = 10, Processed = false };

            var series = new PlayoffSeries() { Home = homeTeam, Away = awayTeam, RequiredWins = 12 };

            series.ProcessGame(game);

            Assert.StrictEqual(1, series.HomeWins);
            Assert.StrictEqual(0, series.AwayWins);
        }

        [Fact]
        public void ShouldProcessAwayWin()
        {
            var homeTeam = new PlayoffTeam() { Id = 15 };
            var awayTeam = new PlayoffTeam() { Id = 12 };

            var game = new PlayoffGame() { Complete = true, Home = homeTeam, Away = awayTeam, HomeScore = 12, AwayScore = 55, Processed = false };

            var series = new PlayoffSeries() { Home = homeTeam, Away = awayTeam, RequiredWins = 12 };

            series.ProcessGame(game);

            Assert.StrictEqual(0, series.HomeWins);
            Assert.StrictEqual(1, series.AwayWins);
        }
    }
}

