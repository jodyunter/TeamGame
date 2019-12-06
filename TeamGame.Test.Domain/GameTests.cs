using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain;
using Xunit;

namespace TeamGame.Test.Domain
{
    public class GameTests
    {
        [Theory]
        [InlineData(0, new int[] {1, 5, 3})]        
        public void ShouldTestGetScore(int difference, int[] data)
        {
            var g = new Game();
            var random = new RandomHelper(data);

            var score = g.GetScore(difference, random);
            Assert.StrictEqual(data[0], score);
            score = g.GetScore(difference, random);
            Assert.StrictEqual(data[1], score);
            score = g.GetScore(difference, random);
            Assert.StrictEqual(data[2], score);
            score = g.GetScore(difference, random);
            Assert.StrictEqual(data[0], score);

        }
    }
}
