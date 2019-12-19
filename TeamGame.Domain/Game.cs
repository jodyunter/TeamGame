using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain
{
    public class Game:IGame
    {
        public int Number { get; set; } = -1;
        public int Day { get; set; } = -1;
        public ITeam Home { get; set; }
        public ITeam Away { get; set; }
        public int HomeScore { get; set; } = 0;
        public int AwayScore { get; set; } = 0;
        public bool CanTie { get; set; } = true;
        public int MaxOverTimePeriods { get; set; } = 0;
        public bool Complete { get; set; } = false;
        public bool Processed { get; set; } = false;
        public Game() { }

        public void Play(Random random)
        {
            int difference = Home.Skill - Away.Skill;

            HomeScore = GetScore(difference, random);
            AwayScore = GetScore(-1 * difference, random);

            int overTimePeriods = 0;
            while (HomeScore == AwayScore && 
                ((MaxOverTimePeriods > overTimePeriods) || !CanTie)
                )
            { 
                    int scoreA = random.Next(Home.Skill);
                    int scoreB = random.Next(Away.Skill);
                    
                    if (scoreA > scoreB) HomeScore++;
                    if (scoreA < scoreB) AwayScore++;

                    overTimePeriods++;
            }

            Complete = true;
        }


        public int GetScore(int difference, Random random)
        {
            int score = random.Next(6 + difference);
            if (score < 0) score = 0;

            return score;
        }

        public override string ToString()
        {
            var formatter = "{0,3}. {1,10}: {2,3} - {3,3} :{4,10}";
            return string.Format(formatter, Number, Home.Name, HomeScore, AwayScore, Away.Name);
        }

        public ITeam GetWinningTeam()
        {
            if (!Complete)
            {
                return null;
            }

            if (HomeScore > AwayScore) return Home;
            else if (AwayScore > HomeScore) return Away;
            else return null;
        }
        public ITeam GetLosingTeam()
        {
            if (!Complete)
            {
                return null;
            }

            if (HomeScore > AwayScore) return Away;
            else if (AwayScore > HomeScore) return Home;
            else return null;
        }


    }
}
