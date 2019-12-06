using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamGame.Domain.Seasons.Rules
{
    public class SeasonRule:IDataObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<SeasonDivisionRule> DivisionRules { get; set; }
        public IList<SeasonTeamRule> TeamRules { get; set; }
        public Season Create(Season previousSeason, int number, int year, int startingDay)
        {

            var season = new Season() { Name = Name, Complete = false, Number = number, Year = year, StartingDay = startingDay, Started = false };

            
            var parentTeamList = TeamRules.Select(t => t.Parent).Distinct().ToList();

            parentTeamList.ForEach(team =>
            {
                
            });
            
        }

    }
}
