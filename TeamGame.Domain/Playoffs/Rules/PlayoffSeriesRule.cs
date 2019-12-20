using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Playoffs.Rules
{
    public class PlayoffSeriesRule
    {
        public string RuleName { get; set; }
        public string SeriesName { get; set; }
        public string HomeTeamFrom { get; set; }        
        public string AwayTeamFrom { get; set; }
        public int HomeTeamValue { get; set; }
        public int AwayTeamValue { get; set; }
        public string HomeRankFrom { get; set; }
        public string AwayRankFrom { get; set; }
        public int RequiredWins { get; set; }
        public string WinnerGoesTo { get; set; }
        public string WinnerRankFrom { get; set; }
        public string LoserGoesTo { get; set; }
        public string LoserRankFrom { get; set; }
    }
}
