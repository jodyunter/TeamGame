using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace TeamGame.Domain.Standing
{
    public class StandingsTeam : ITeam,IComparable<StandingsTeam>
    {                
        public Team Parent { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; } = -1;
        public int Skill { get; set; } = 5;
        public int Wins { get; set; } = 0;
        public int Loses { get; set; } = 0;
        public int Ties { get; set; } = 0;
        public int GoalsFor { get; set; } = 0;
        public int GoalsAgainst { get; set; } = 0;
        public int GoalDifference { get { return GoalsFor - GoalsAgainst; } }
        public int Points { get { return Wins * PointsForWins + Ties * PointsForTies; } }
        public int PointsForWins { get; set; } = 2;
        public int PointsForTies { get; set; } = 1;
        public int GamesPlayed { get { return Wins + Loses + Ties; } } 
        
        public StandingsTeam() { }

        public StandingsTeam(Team parent, string name, int rank, int skill, int wins, int loses, int ties, int goalsFor, int goalsAgainst, int pointsForWins, int pointsForTies)
        {
            Parent = parent;
            Name = name;
            Rank = rank;
            Skill = skill;
            Wins = wins;
            Loses = loses;
            Ties = ties;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
            PointsForWins = pointsForWins;
            PointsForTies = pointsForTies;
        }

        public void ProcessWin(int us, int them)
        {
            ProcessGoals(us, them);
            Wins++;
        }

        public void ProcessLoss(int us, int them)
        {
            ProcessGoals(us, them);
            Loses++;
        }

        public void ProcessTie(int us, int them)
        {
            ProcessGoals(us, them);
            Ties++;
        }
        public void ProcessGoals(int us, int them)
        {
            GoalsAgainst += them;
            GoalsFor += us;
        }

        public override string ToString()
        {
            var formatter = Standings.STANDINGS_FORMATTER;

            return string.Format(formatter, Rank, Name, Wins, Loses, Ties, Points, GamesPlayed, GoalsFor, GoalsAgainst, GoalDifference, Skill);
        }
        

        //may not need this, just use LINQ queries when needed
        public int CompareTo([AllowNull] StandingsTeam other)
        {
            if (other == null) return -1;

            if (Points == other.Points)
            {
                if (GamesPlayed == other.GamesPlayed)
                {
                    if (Wins == other.Wins)
                    {
                        if (GoalDifference == other.GoalDifference)
                        {
                            return GoalsFor - other.GoalsFor;
                        }
                        else 
                            return GoalDifference - other.GoalDifference;
                    }
                    else 
                        return Wins - other.Wins;
                }
                else
                    return other.GamesPlayed - GamesPlayed;
            }
            else
                return Points - other.Points;
        }
    }
}
