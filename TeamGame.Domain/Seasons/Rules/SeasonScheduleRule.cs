using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Seasons.Rules
{
    public class SeasonScheduleRule
    {
        public int RuleType { get; set; }//turn to enum 
        public string RuleName { get; set; }
        public SeasonDivisionRule HomeGroup { get; set; }  //if divisional we don't need a level number but we may need a rank
        public SeasonDivisionRule AwayGroup { get; set; }
        public Team ParentHomeTeam { get; set; }
        public Team ParentAwayTeam { get; set; }
        public int PreviousHomeRank { get; set; }
        public int PreviousAwayRank { get; set; }
        public int Iterations { get; set; } //each iteration is one applicaiton of the home vs away group
        public bool HomeAndAway { get; set; } //if true then the games are doubled         

    }

    public enum RuleType
    {
        Divisional = 0,
        Team = 1,
        PreviousDivisionRank = 2
    }
}
