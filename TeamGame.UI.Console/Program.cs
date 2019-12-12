using System;
using TeamGame.Domain;
using System.Linq;
using System.Collections.Generic;
using TeamGame.Domain.Scheduling;
using TeamGame.Domain.Standing;
using TeamGame.Domain.Seasons;
using TeamGame.Domain.Competitions;

namespace TeamGame.UI.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var season = new Season()
            {
                Number = 1,
                Year = 1,
                Name = "First Season"
            };

            var league = new SeasonDivision("League", season, DivisionLevel.League, null, null, null);
            var conferenceA = new SeasonDivision("Conference A", season, DivisionLevel.Conference, league, null, null);
            var conferenceB = new SeasonDivision("Conference B", season, DivisionLevel.Conference, league, null, null);
            var divisionA = new SeasonDivision("Division A", season, DivisionLevel.Division, conferenceA, null, null);
            var divisionB = new SeasonDivision("Division B", season, DivisionLevel.Division, conferenceA, null, null);
            var divisionC = new SeasonDivision("Division C", season, DivisionLevel.Division, conferenceB, null, null);
            var divisionD = new SeasonDivision("Division D", season, DivisionLevel.Division, conferenceB, null, null);
            var provA = new SeasonDivision("Province A", season, DivisionLevel.Province, null, null, null);
            var cityDivA = new SeasonDivision("City A", season, DivisionLevel.City, provA, null, null);
            var cityDivB = new SeasonDivision("City B", season, DivisionLevel.City, provA, null, null);


            for (int i = 0; i < 20; i++)
            {
                var team = new SeasonTeam() { Parent = new Team(i, "Team " + i, 5), Name = "Team " + i };

                switch (i % 4)
                {
                    case 0:
                        divisionA.AddTeam(team);                 
                        break;
                    case 1:
                        divisionB.AddTeam(team);
                        break;
                    case 2:                        
                        divisionC.AddTeam(team);
                        break;
                    case 3:
                        divisionD.AddTeam(team);
                        break;
                }

                if (i % 6 == 0)
                {
                    cityDivA.AddTeam(team);
                }

                if (i % 7 == 0 && i != 0)
                {
                    cityDivB.AddTeam(team);
                }
            }


            /*
            provA.AddChildDivision(cityDivA);
            provA.AddChildDivision(cityDivB);
            conferenceA.AddChildDivision(divisionA);
            conferenceA.AddChildDivision(divisionB);
            conferenceB.AddChildDivision(divisionC);
            conferenceB.AddChildDivision(divisionD);
            league.AddChildDivision(conferenceA);
            league.AddChildDivision(conferenceB);
            */
            season.AddDivision(league);
            season.AddDivision(provA);
            

            var seasonRules = new SeasonGameCreator(season, true, 1);            
            var divASched = Scheduler.CreateGames(1, 1, 1, divisionA.Teams.ToList<ITeam>(), 3, true, seasonRules); //24 games
            var divBSched = Scheduler.CreateGames(1, 1, 1, divisionB.Teams.ToList<ITeam>(), 3, true, seasonRules);
            var divCSched = Scheduler.CreateGames(1, 1, 1, divisionC.Teams.ToList<ITeam>(), 3, true, seasonRules);
            var divDSched = Scheduler.CreateGames(1, 1, 1, divisionD.Teams.ToList<ITeam>(), 3, true, seasonRules);
            var confASched = Scheduler.CreateGames(1, 1, 1, conferenceA.Teams.ToList<ITeam>(), 1, true, seasonRules); //18 games
            var confBSched = Scheduler.CreateGames(1, 1, 1, conferenceB.Teams.ToList<ITeam>(), 1, true, seasonRules);
            
            var interConfSched = Scheduler.CreateGames(1, 1, 1, league.Teams.ToList<ITeam>(), 1, true, seasonRules); //38 games

            var schedule = new Schedule();

            Scheduler.MergeSchedules(schedule, divASched, divBSched, divCSched, divDSched, confASched, confBSched, interConfSched);

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

            season.GetStandingsByDivisionLevel(DivisionLevel.Division).ToList().ForEach(standings =>
            {
                Console.WriteLine(standings);
            });

            season.GetStandingsByDivisionLevel(DivisionLevel.City).ToList().ForEach(standings =>
            {
                Console.WriteLine(standings);
            });

            season.GetStandingsByDivisionLevel(DivisionLevel.Province).ToList().ForEach(standings =>
            {
                Console.WriteLine(standings);
            });


            Console.ReadLine();
        }
    }
}
