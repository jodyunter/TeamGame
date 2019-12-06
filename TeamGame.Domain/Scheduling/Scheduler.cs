using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamGame.Domain.Scheduling
{
    //todo stop using the dictionaries and start using Schedules and merging scheudles instead
    public class Scheduler
    {

        /* these are the two entry methods if multiple iterations of a series of games are needed */
        public static Schedule CreateGames(int year, int lastGameNumber, int startDay, List<ITeam> teams, int iterations, bool homeAndAway, IGameCreator gameCreator)
        {
            return CreateGames(year, lastGameNumber, startDay, teams, null, iterations, homeAndAway, gameCreator);
        }
        public static Schedule CreateGames(int year, int lastGameNumber, int startDay, List<ITeam> homeTeams, List<ITeam> awayTeams, int iterations, bool homeAndAway, IGameCreator gameCreator)
        {
            var result = new Schedule();

            for (int i = 0; i < iterations; i++)
            {
                if (awayTeams == null || awayTeams.Count == 0) lastGameNumber = MergeSchedules(result, CreateGamesSingleGroup(year, lastGameNumber, startDay, homeTeams, homeAndAway, gameCreator));
                else lastGameNumber = MergeSchedules(result, CreateGamesTwoDifferentGroups( year, lastGameNumber, startDay, homeTeams, awayTeams, homeAndAway, gameCreator));
            }

            //this will overwrite whatever game number work was done in the other methods
            foreach (KeyValuePair<int, ScheduleDay> data in result.Days)
            {
                lastGameNumber = UpdateGameNumbers(lastGameNumber, data.Value);
            }

            return result;
        }


        public static int GetLastDay(Schedule schedule)
        {
            return schedule.Days.Keys.Max();
        }

        //Assumption is that they can add days afterwards.  Different methods need to handle adding games to already established days
        //todo need to rework this
        public static Schedule CreateGamesTwoDifferentGroups(int year, int lastGameNumber, int startDay, List<ITeam> homeTeams, List<ITeam> awayTeams, bool homeAndAway, IGameCreator gameCreator)
        {

            int initialDays = 0;
            var aTeamList = new List<ITeam>();
            var bTeamList = new List<ITeam>();
            bool reverseHomeAndAway = false;

            //the larger team list has to be the top loop
            if (homeTeams.Count >= awayTeams.Count)
            {
                aTeamList = homeTeams;
                bTeamList = awayTeams;
                initialDays = homeTeams.Count;
            }
            else
            {
                bTeamList = homeTeams;
                aTeamList = awayTeams;
                reverseHomeAndAway = true;
                initialDays = awayTeams.Count;
            }

            int maxDay = initialDays + startDay - 1;

            var schedule = new Schedule();

            for (int i = 0 + startDay; i <= maxDay; i++)
            {
                schedule.AddDay(i);
            }

            int currentDay = startDay;
            int teamStartDay = currentDay;

            aTeamList.ForEach(aTeam =>
            {
                currentDay = teamStartDay;
                //for each home team loop through the away teams, increment the day by one and add the next game.
                bTeamList.ForEach(bTeam =>
                {
                    var game = gameCreator.CreateGame(reverseHomeAndAway ? bTeam : aTeam, reverseHomeAndAway ? aTeam : bTeam);
                    schedule.Days[currentDay].AddGame(game);

                    currentDay = GetNextDay(currentDay, startDay, maxDay, 1);

                });
                //when done the home team games, start on the previous day the last team started on and go through it again
                teamStartDay = GetNextDay(teamStartDay, startDay, maxDay, -1);
            });


            if (homeAndAway) CreateAwayGamesForHomeAndAway(schedule, maxDay + 1, gameCreator);
            //do the game numbers last so that they are in order by day
            foreach (KeyValuePair<int, ScheduleDay> data in schedule.Days)
            {
                lastGameNumber = UpdateGameNumbers(lastGameNumber, data.Value);
            }

            return schedule;

        }

        private static int UpdateGameNumbers(int lastGameNumber, ScheduleDay day)
        {
            lastGameNumber++;

            day.Games.ToList().ForEach(g => { g.Number = lastGameNumber; lastGameNumber++; });

            return lastGameNumber;
        }

        public static int GetNextDay(int currentDay, int startDay, int maxDay, int daysToIncrement)
        {
            //normalize to zero first
            if (currentDay < startDay || currentDay > maxDay) throw new ApplicationException("Invalid Current Day value.");

            currentDay -= startDay;
            var totalDays = maxDay - startDay + 1;

            currentDay += daysToIncrement;

            currentDay = Mod(currentDay, totalDays);

            currentDay += startDay;

            return currentDay;
        }

        private static int Mod(int k, int n) { return ((k %= n) < 0) ? k + n : k; }


        public static int MergeSchedules(Schedule initial, params Schedule[] schedulesToMerge)
        {
            int result = -1;

            schedulesToMerge.ToList().ForEach(schedule =>
            {
                result = MergeSchedules(initial, schedule);
            });

            return result;
        }
        //this returns the last day
        //todo: when we had new games to already established days
        //we assume no team can play more than once a day
        public static int MergeSchedules(Schedule initial, Schedule newDays)
        {

            newDays.Days.Keys.ToList().ForEach(dayNumber =>
            {
                int initialDayNumber = dayNumber;

                if (!initial.Days.ContainsKey(dayNumber)) initial.AddDay(dayNumber);
                else
                {
                    //todo how many games can a team play on a given day
                    if (initial.Days[dayNumber].DoesAnyTeamPlayInDay(newDays.Days[dayNumber]))
                    {
                        initialDayNumber = initial.Days.Keys.Max() + 1;
                        initial.AddDay(initialDayNumber);

                    }
                }
                initial.Days[initialDayNumber].AddGamesToDay(newDays.Days[dayNumber].Games.ToList());

            });

            return initial.Days.Keys.Max();
        }

        public static int MergeSchedulesTryToCompress(Schedule initial, Schedule newDays)
        {
            //no more days to merge
            if (newDays.Days.Count == 0)
            {
                return initial.Days.Keys.Max();
            }

            //this is the first schedule to merge
            if (initial.Days.Count == 0)
            {
                newDays.Days.Values.ToList().ForEach(day =>
                {
                    initial.AddDay(day.Day);
                    initial.Days[day.Day].AddGamesToDay(day.Games.ToList());
                });
                return initial.Days.Keys.Max();
            }

            var added = new Dictionary<int, bool>();



            initial.Days.Values.ToList().ForEach(currentDay =>
            {
                var dayList = newDays.Days.Keys.ToList();

                //try to add each day
                dayList.ForEach(dayNumber =>
                {
                    if (!currentDay.DoesAnyTeamPlayInDay(newDays.Days[dayNumber]))
                    {
                        currentDay.AddGamesToDay(newDays.Days[dayNumber].Games.ToList());
                        newDays.Days.Remove(dayNumber);
                    }
                });
            });

            //add one more day then try to recompress
            if (newDays.Days.Count > 0)
            {
                int lastDay = initial.Days.Keys.Max();

                initial.AddDay(lastDay + 1);
                initial.Days[lastDay + 1].AddGamesToDay(newDays.Days[newDays.Days.Keys.ToList()[0]].Games.ToList());

                newDays.Days.Remove(newDays.Days.Keys.ToList()[0]);

                return MergeSchedulesTryToCompress(initial, newDays);
            }
            else
            {
                return initial.Days.Keys.Max();
            }
        }

        public static int AddGamesToSchedule(Schedule schedule, List<Game> games, int dayToStartOn)
        {
            int result = 0;

            if (dayToStartOn < 1)
            {
                var nextInCompleteDay = schedule.GetNextNotStartedDay();
                if (nextInCompleteDay == null) dayToStartOn = schedule.Days.Keys.Max() + 1;
                else
                    dayToStartOn = schedule.GetNextNotStartedDay().Day;
            }
            games.ForEach(g => { result = AddGameToSchedule(schedule, g, dayToStartOn); });

            return result;
        }
        public static int AddGameToSchedule(Schedule schedule, Game game, int dayToStartOn)
        {
            bool foundDayToAddTeamTo = false;
            int currentDay = dayToStartOn;
            int dayToAddTeamTo = currentDay;
            int maxDay = schedule.Days.Count == 0 ? dayToStartOn : schedule.Days.Keys.Max();

            while (!foundDayToAddTeamTo && currentDay <= maxDay)
            {
                if (schedule.Days.ContainsKey(currentDay))
                {
                    bool canAdd = true;
                    canAdd = canAdd && !schedule.Days[currentDay].DoesTeamPlayInDay(game.Home);
                    canAdd = canAdd && !schedule.Days[currentDay].DoesTeamPlayInDay(game.Away);

                    if (canAdd) foundDayToAddTeamTo = true;

                    dayToAddTeamTo = currentDay;
                }

                currentDay++;
            }

            if (!foundDayToAddTeamTo)
            {
                schedule.AddDay(currentDay);
                dayToAddTeamTo = currentDay;
            }

            schedule.Days[dayToAddTeamTo].AddGame(game);

            return currentDay;
        }

        public static Schedule CreateGamesSingleGroup(int year, int lastGameNumber, int startDay, IList<ITeam> teams, bool homeAndAway, IGameCreator gameCreator)
        {

            int[,] array = CreateArrayForScheduling(teams.Count);

            int totalDays = teams.Count % 2 == 0 ? teams.Count - 1 : teams.Count;


            var schedule = new Schedule();

            for (int i = 0; i < totalDays; i++) schedule.AddDay(i + startDay);
            //agorithm for swapping the values


            for (int d = startDay; d < totalDays + startDay; d++)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    if (array[i, 0] != -1)
                    {
                        var g = gameCreator.CreateGame(teams[array[i, 0]], teams[array[i, 1]]);
                        schedule.Days[d].AddGame(g);
                    }

                }

                array = ProcessNextPosition(array);
            }

            if (homeAndAway) CreateAwayGamesForHomeAndAway(schedule, totalDays + startDay, gameCreator);

            foreach (KeyValuePair<int, ScheduleDay> data in schedule.Days)
            {
                lastGameNumber = UpdateGameNumbers(lastGameNumber, data.Value);
            }

            return schedule;
        }

        public static Schedule CreateAwayGamesForHomeAndAway(Schedule schedule, int dayToStartHomeAndAway, IGameCreator gameCreator)
        {
            int count = 0;

            schedule.Days.Keys.ToList().ForEach(dayNumber =>
            {
                int newDay = dayToStartHomeAndAway + count;
                if (!schedule.Days.ContainsKey(newDay)) schedule.Days.Add(newDay, new ScheduleDay(dayToStartHomeAndAway + count));

                schedule.Days[dayNumber].Games.ToList().ForEach(game =>
                {
                    var g = gameCreator.CreateGame(game.Away, game.Home);
                    schedule.Days[newDay].Games.Add(g);

                });

                count++;
            });
            return schedule;
        }

        //these methods setup an array to guarantee no duplicate games.
        //we pick a pivot and we rotate around it.
        //for even number of teams, one team is the pivot.
        //for odd number of teams, a -1 is the pivot and the team paired with them gets a day off that day
        public static int[,] CreateArrayForScheduling(int teams)
        {
            int firstSpot = 0;
            int size = -1;
            if (teams % 2 == 0)
            {
                size = teams / 2;
            }
            else
            {
                firstSpot = -1;
                size = teams / 2 + 1;

            }

            int[,] newArray = new int[size, 2];

            newArray[0, 0] = firstSpot;

            for (int i = 0; i < size; i++)
            {
                newArray[i, 1] = teams - 1 - i;
                if (i == 0) newArray[0, 0] = firstSpot;
                else newArray[i, 0] = firstSpot + i;
            }

            return newArray;

        }
        public static int[,] ProcessNextPosition(int[,] array)
        {

            int[,] oldArray = (int[,])array.Clone();
            int size = array.GetLength(0);

            array[0, 0] = oldArray[0, 0]; //keep the pivot point

            for (int i = 0; i < size; i++)
            {

                if (i == 0)
                {
                    array[0, 0] = oldArray[0, 0];
                    array[0, 1] = oldArray[1, 1];
                }
                else if (i == 1)
                {
                    array[1, 0] = oldArray[0, 1];
                    if (size == 2) array[1, 1] = oldArray[1, 0];
                    else array[1, 1] = oldArray[2, 1];
                }
                else if (i == size - 1)
                {
                    array[size - 1, 0] = oldArray[size - 2, 0];
                    array[size - 1, 1] = oldArray[size - 1, 0];
                }
                else
                {
                    array[i, 0] = oldArray[i - 1, 0];
                    array[i, 1] = oldArray[i + 1, 1];
                }

            }


            return array;
        }


    }
}
