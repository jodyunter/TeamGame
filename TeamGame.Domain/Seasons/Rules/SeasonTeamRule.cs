using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Seasons.Rules
{
    public class SeasonTeamRule
    {
        //for multiple divisions, just put mulitple team rules with the same parent
        public Team Parent { get; set; }
        public SeasonDivisionRule Division { get; set; }
        public bool Active { get; set; }

        public SeasonTeamRule() { }

        public SeasonTeamRule(Team parent, SeasonDivisionRule division, bool active)
        {
            Parent = parent;
            Division = division;
            Active = active;
        }
    }
}
