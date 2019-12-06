using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Seasons.Rules
{
    public class SeasonRule:DataObject
    {
        public string Name { get; set; }
        public IList<SeasonDivisionRule> DivisionRules { get; set; }
        public IList<SeasonTeamRule> TeamRules { get; set; }
        public Season Create(Season previousSeason)
        {
            
            return null;
        }
    }
}
