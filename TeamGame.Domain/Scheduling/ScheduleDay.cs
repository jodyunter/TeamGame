using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamGame.Domain.Scheduling
{
    public class ScheduleDay
    {
        public IList<Game> Games { get; set; } = new List<Game>();
        public int Day { get; set; } = -1;

        public void AddGame(Game game)
        {
            game.Day = Day;
            Games.Add(game);
        }
        public bool DoesTeamPlayInDay(ITeam team)
        {
            bool found = false;

            Games.ToList().ForEach(g =>
            {
                if (g.Home.Name.Equals(team.Name) || g.Away.Name.Equals(team.Name)) found = true;
            });

            return found;

        }

        public bool IsCompleteAndProcessed()
        {
            var games = Games.Where(g => !g.Complete || !g.Processed).FirstOrDefault();

            if (games == null) return true;
            return false;
        }

        public ScheduleDay(int dayNumber)
        {
            Day = dayNumber;
        }
        public ScheduleDay() { }

        public override string ToString()
        {
            var result = "Day " + Day;

            Games.ToList().ForEach(g =>
            {
                result += "\n" + g;
            });

            return result;
        }

        public bool IsComplete()
        {
            return Games.ToList().TrueForAll(g => g.Complete && g.Processed);
        }

        public bool IsStarted()
        {
            return Games.ToList().TrueForAll(g => !g.Complete && !g.Processed);
        }


        public void AddGamesToDay(List<Game> games)
        {
            games.ForEach(g =>
            {
                g.Day = Day;
                Games.Add(g);
            });
        }

        public bool DoesAnyTeamPlayInDay(ScheduleDay other)
        {
            var result = false;

            other.Games.ToList().ForEach(game =>
            {
                result = result || DoesTeamPlayInDay(game.Home) || DoesTeamPlayInDay(game.Away);
            });

            return result;
        }
    }
}

