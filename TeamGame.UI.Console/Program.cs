using System;
using TeamGame.Domain;
using System.Linq;
using System.Collections.Generic;
using TeamGame.Domain.Scheduling;
using TeamGame.Domain.Standing;
using TeamGame.Domain.Seasons;
using TeamGame.Domain.Competitions;
using TeamGame.UI.ConsoleApp.TestData;
using TeamGame.Domain.Seasons.Scheduling;
using TeamGame.Domain.Seasons.Rules;

namespace TeamGame.UI.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var seasonRule = DataSetup.SetupSeasonRule(10);

            var season = seasonRule.Create(null, 1, 1, 1, new SeasonGameCreator(null, true, 1));

            

            var seasonRules = new SeasonGameCreator(season, true, 1);

            var schedule = SeasonScheduler.CreateGamesByRule(seasonRule.ScheduleRules[0], season);

            //var schedule = new Schedule();

            //Scheduler.MergeSchedules(schedule, divASched, divBSched, divCSched, divDSched, confASched, confBSched, interConfSched);

            var random = new Random();

            while (!schedule.IsComplete())
            {
                var day = schedule.GetNextInCompleteDay();

                day.Games.ToList().ForEach(g =>
                {
                    if (!g.Complete)
                    {
                        g.Play(random);
                    }

                    if (!g.Processed)
                    {
                        if (g is ICompetitionGame) 
                            ((ICompetitionGame)g).Competition.ProcessGame((ICompetitionGame)g);
                    }
                });
            }

            season.UpdateRankings();

            season.GetStandingsByDivisionLevel(DivisionLevel.League).ToList().ForEach(standings =>
            {
                Console.WriteLine(standings);
            });



            Console.ReadLine();
        }
    }
}
