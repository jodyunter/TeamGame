using System;
using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain.Scheduling;
using TeamGame.Domain.Seasons.Rules;

namespace TeamGame.Domain.Seasons.Scheduling
{
    public class SeasonScheduler:Scheduler
    {
        public static Schedule CreateGamesByRule(SeasonScheduleRule rule, Season season)
        {
            var schedule = season.Schedule();

            switch(rule.RuleType)
            {
                case SeasonScheduleRuleType.Divisional:
                    var homeTeams = season.Divisions.Where(d => d.Name.Equals(rule.HomeGroup.Name)).First().Teams.ToList<ITeam>();
                    var awayTeams = rule.AwayGroup == null ? null : season.Divisions.Where(d => d.Name.Equals(rule.AwayGroup.Name)).FirstOrDefault().Teams.ToList<ITeam>();

                    var newSchedule = CreateGames(season.Year, -1, 1, homeTeams, awayTeams, rule.Iterations, rule.HomeAndAway, season.GameCreator);

                    if (schedule.Days.Count == 0)
                    {
                        schedule = newSchedule;
                    }
                    else
                    {
                        MergeSchedules(schedule, newSchedule);
                    }

                    break;
                case SeasonScheduleRuleType.DivisionLevel:
                    var level = rule.DivisionLevel;

                    var divisionsAtLevel = season.Divisions.Where(d => d.Level == level).ToList();

                    divisionsAtLevel.ForEach(division =>
                    {
                        var schedulingTeams = division.Teams.ToList<ITeam>();

                        var newSchedule = CreateGames(season.Year, -1, 1, schedulingTeams, rule.Iterations, rule.HomeAndAway, season.GameCreator);

                        if (schedule.Days.Count == 0)
                        {
                            schedule = newSchedule;
                        }
                        else
                        {
                            MergeSchedules(schedule, newSchedule);
                        }
                    });
                    break;
                default:
                    throw new SeasonScheduleException("Rule has not been implemented yet: " + rule.RuleType);
            }

            return schedule;
        }
    }
}
