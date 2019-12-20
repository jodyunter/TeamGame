using System;
using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain.Competitions;

namespace TeamGame.Domain.Playoffs.Rules
{
    public class PlayoffGroupRule
    {
        public string RuleName { get; set; }
        public string GroupName { get; set; }
        public string FromCompetitionName { get; set; }
        public int FirstRank { get; set; }
        public int LastRank { get; set; }

        //get the teams from the competition and add them to the group
        public void AddTeamsFromRuleToCompetition(ICompetition oldCompetition, Playoff playoff)
        {
            if (oldCompetition.Name.Equals(FromCompetitionName))
            {
                var oldRankings = oldCompetition.Rankings.Where(r => r.Group.Equals(GroupName) && r.Rank >= FirstRank && r.Rank <= LastRank).ToList();

                var newRankings = new List<PlayoffRanking>();

                oldRankings.ForEach(oldRank =>
                {
                    playoff.AddRanking(oldRank.Team.Name, GroupName, oldRank.Rank);
                });
            }
            else
            {
                throw new PlayoffException("Can't add competition " + oldCompetition.Name + " expecting: " + FromCompetitionName);
            }
        }
    }
}
