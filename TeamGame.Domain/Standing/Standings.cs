using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamGame.Domain.Standing
{
    public class Standings
    {
        public static string STANDINGS_FORMATTER = "{0,3}. {1,-13}{2,3}{3,3}{4,4}{5,4}{6,4}{7,4}{8,4}{9,5}{10,5}";
        public string Name { get; set; }        
        public IList<StandingsTeam> Teams { get; set; }
        public static bool ProcessGame(IGame g)
        {
            if (g.Complete)
            {
                if ((g.Home is StandingsTeam) && (g.Away is StandingsTeam))
                {
                    var home = (StandingsTeam)g.Home;
                    var away = (StandingsTeam)g.Away;
                    if (g.HomeScore > g.AwayScore)
                    {
                        home.ProcessWin(g.HomeScore, g.AwayScore);
                        away.ProcessLoss(g.AwayScore, g.HomeScore);
                    }
                    else if (g.HomeScore < g.AwayScore)
                    {
                        home.ProcessLoss(g.HomeScore, g.AwayScore);
                        away.ProcessWin(g.AwayScore, g.HomeScore);
                    }
                    else
                    {
                        home.ProcessTie(g.HomeScore, g.AwayScore);
                        away.ProcessTie(g.AwayScore, g.HomeScore);
                    }
                    g.Processed = true;
                    return true;
                }
                else
                {
                    throw new StandingsException("These are not standings teams.");
                }
            }
            else
            {
                throw new StandingsException("Game is not complete.");
            }
         
        }

        public static IList<StandingsTeam> SortTeams(IList<StandingsTeam> teams)
        {
            return teams.ToList().OrderByDescending(k => k.Points).ThenBy(k => k.GamesPlayed).ThenByDescending(k => k.GoalDifference).ThenByDescending(k => k.Wins).ThenByDescending(k => k.GoalsFor).ToList();
        }

        public void ProcessRank()
        {
            int rank = 1;
            SortTeams(Teams)
                .ToList().ForEach(t =>
            {
                t.Rank = rank;
                rank++;
            });
        }
        public override string ToString()
        {
            var result = Name + "\n";

            var header = string.Format(STANDINGS_FORMATTER, "R", "Name", "W", "L", "T", "Pts", "GP", "GF", "GA", "GD", "Sk");

            result += header + "\n";
            Teams.OrderBy(k => k.Rank).ToList().ForEach(team =>
            {
                result += team + "\n";
            });

            return result;
        }
    }
}
