using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Seasons.Rules
{
    public class SeasonScheduleRule
    {
        public SeasonScheduleRuleType RuleType { get; set; }//turn to enum 
        public string RuleName { get; set; }
        public SeasonDivisionRule HomeGroup { get; set; }  //if divisional we don't need a level number but we may need a rank
        public SeasonDivisionRule AwayGroup { get; set; }
        public Team ParentHomeTeam { get; set; }
        public Team ParentAwayTeam { get; set; }
        public DivisionLevel DivisionLevel { get; set; }
        public int PreviousHomeRank { get; set; } //use these if you always want rank 1 from a divion to play rank 2 from a division in the group
        public int PreviousAwayRank { get; set; }
        public int Iterations { get; set; } //each iteration is one applicaiton of the home vs away group
        public bool HomeAndAway { get; set; } //if true then the games are doubled         

    }

    public enum SeasonScheduleRuleType
    {
        Divisional = 0,
        Team = 1,
        PreviousDivisionRank = 2,
        DivisionLevel = 3 //for when you want get all level 2 teams to play all teams in their level 2 group
    }
}
