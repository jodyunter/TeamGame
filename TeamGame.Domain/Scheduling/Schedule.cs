using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamGame.Domain.Scheduling
{
    public class Schedule
    {

        public Dictionary<int, ScheduleDay> Days { get; set; } = new Dictionary<int, ScheduleDay>();

        public void SetupDays(IList<Game> games)
        {
            Days = new Dictionary<int, ScheduleDay>();
            games.ToList().ForEach(game =>
            {
                if (!Days.ContainsKey(game.Day))
                {
                    Days[game.Day] = new ScheduleDay() { Day = game.Day };
                }

                Days[game.Day].AddGame(game);
            });
        }

        public void AddDay(int dayNumber)
        {
            if (Days.ContainsKey(dayNumber)) throw new ApplicationException("Day already exists");
            Days.Add(dayNumber, new ScheduleDay(dayNumber));
        }

        public bool DoesTeamPlayInDay(ITeam team, int day)
        {
            if (!Days.ContainsKey(day)) return false;
            else
            {
                return Days[day].DoesTeamPlayInDay(team);
            }
        }

        public IList<Game> GetGames()
        {
            var result = new List<Game>();

            Days.Values.ToList().ForEach(day =>
            {
                result.AddRange(day.Games);
            });

            return result;
        }

        public ScheduleDay GetNextDay()
        {
            var day = Days.Values.OrderBy(x => x.Day).Where(x => !x.IsCompleteAndProcessed()).FirstOrDefault();

            return day;
        }

        public ScheduleDay GetNextInCompleteDay()
        {
            ScheduleDay nextDay = null;

            var list = Days.Keys.ToList();
            list.Sort();
            bool found = false;
            for (int i = 0; i < list.Count && !found; i++)
            {
                if (!Days[list[i]].IsComplete())
                {
                    found = true;
                    nextDay = Days[list[i]];
                }
            }

            return nextDay;

        }

        public ScheduleDay GetNextNotStartedDay()
        {
            ScheduleDay nextDay = null;

            var list = Days.Keys.ToList();
            list.Sort();
            bool found = false;
            for (int i = 0; i < list.Count && !found; i++)
            {
                if (!Days[list[i]].IsStarted())
                {
                    found = true;
                    nextDay = Days[list[i]];
                }
            }

            return nextDay;
        }

        public bool IsComplete()
        {
            return GetNextInCompleteDay() == null;
        }
    }
}
