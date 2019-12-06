﻿using System;
using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain.Scheduling;
using TeamGame.Domain.Seasons.Rules;

namespace TeamGame.Domain.Seasons.Scheduling
{
    public class SeasonScheduleCounter
    {
        //exceptions should really be validations
        public Dictionary<Team, SeasonScheduleTeamCounter> TeamCounts { get; set; } = new Dictionary<Team, SeasonScheduleTeamCounter>();

        public void CountByRule(SeasonScheduleRule rule)
        {
            if (rule.ParentHomeTeam != null)
            {                
                //Home vs Away
                if (rule.ParentAwayTeam != null)
                {                    
                    AddGame(rule.ParentHomeTeam, rule.ParentAwayTeam, rule.HomeAndAway);                
                }
                //Home vs AwayGroup
                else if (rule.AwayGroup != null)
                {
                    AddGames(rule.ParentHomeTeam, rule.AwayGroup.GetTeamsInDivision(), true, rule.HomeAndAway);                    
                }
                else
                {
                    //validation error
                    throw new SchedulingException("Rule : " + rule.RuleName + " has a Parent Home Team but no  Away opponent");
                }
            }
            else if (rule.ParentAwayTeam != null)
            {
                if (rule.HomeGroup == null)
                {
                    throw new SchedulingException("Rule : " + rule.RuleName + " has a Parent Away Team but no Home opponent");
                }
                //Away vs HomeGroup                
                else
                {
                    AddGames(rule.ParentAwayTeam, rule.HomeGroup.GetTeamsInDivision(), false, rule.HomeAndAway);
                }
            }
            else
            {
                if (rule.HomeGroup == null)
                {
                    throw new SchedulingException("Rule : " + rule.RuleName + " has no teams and no home group. ");
                }
                //HomeGroup vs AwayGroup or HomeGroup vs HomeGroup
                else
                {
                    var homeTeams = rule.HomeGroup.GetTeamsInDivision().ToList();

                    if (rule.AwayGroup != null)
                    {
                        var awayTeams = rule.AwayGroup.GetTeamsInDivision().ToList();

                        homeTeams.ForEach(t =>
                        {
                            AddGames(t, awayTeams, true, rule.HomeAndAway);
                        });
                    }
                    else
                    {
                        for (int i = 0; i < homeTeams.Count - 1; i++)
                        {
                            for (int j = i + 1; j < homeTeams.Count; j++) 
                            {
                                var homeTeam = homeTeams[i];
                                var awayTeam = homeTeams[j];

                                AddGame(homeTeam, awayTeam, rule.HomeAndAway);
                            }
                        }
                    }
                }
            }

        }

        public void AddGames(Team team, IList<Team> opponents, bool isHome, bool homeAndAway)
        {
            opponents.ToList().ForEach(opponent =>
            {                
                AddGame(isHome ? team : opponent, isHome ? opponent : team, homeAndAway);
            });
        }

        public void AddGame(Team home, Team away, bool isHomeAndAway)
        {
            if (home.Id != away.Id)
            {
                SetupTeam(home);
                SetupTeam(away);
                TeamCounts[home].AddGameVsTeam(away, true, isHomeAndAway);
                TeamCounts[away].AddGameVsTeam(home, isHomeAndAway, true);
            }
        }
        public void SetupTeam(Team team)
        {
            if (!TeamCounts.ContainsKey(team))
            {
                TeamCounts[team] = new SeasonScheduleTeamCounter(team);
            }
        }
    }

    public class SeasonScheduleTeamCounter
    {
        public Team Parent { get; set; }
        public int HomeGames { get; set; }
        public int AwayGames { get; set; }
        public Dictionary<Team, int> HomeGamesVsTeams { get; set; }
        public Dictionary<Team, int> AwayGamesVsTeams { get; set; }

        public SeasonScheduleTeamCounter(Team team)
        {
            HomeGamesVsTeams = new Dictionary<Team, int>();
            AwayGamesVsTeams = new Dictionary<Team, int>();
            HomeGames = 0;
            AwayGames = 0;
            Parent = team;
        }
        public void AddGameVsTeam(Team opponent, bool isHome, bool isAway)
        {
            if (Parent.Id == opponent.Id)
            {
                throw new SchedulingException("Can't add game vs team if opponent is same team!");
            }

            if (isHome)
                AddGame(HomeGamesVsTeams, opponent);

            if (isAway)
                AddGame(AwayGamesVsTeams, opponent);
        }

        public void AddGame(Dictionary<Team, int> list, Team opponent)
        {
            if (!list.ContainsKey(opponent))
            {
                list[opponent] = 0;
            }

            list[opponent]++;
        }
    }
}
