using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamGame.Domain.Playoffs
{
    public class PlayoffRanking:IRanking
    {
        public int Rank { get; set; }   
        public int Round { get; set; }//so we know when we expect the groups to be complete, maybe create playoff divisions!
        public PlayoffGroup Group { get; set; }

        public ITeam Team
        {
            get { return Team; }
            set
            {
                if (value is PlayoffTeam)
                {
                    Team = (PlayoffTeam)value;
                }
                else
                {
                    throw new PlayoffException("Can't add a non season team to a season team ranking");
                }
            }
        }

        //this will reorder the rankings for this group based on the current values
        public static void UpdateRanksForCompleteGroup(IList<PlayoffRanking> rankingGroup)
        {
            int currentRank = 1;
            rankingGroup.OrderBy(r => r.Rank).ToList().ForEach(ranking =>
            {
                ranking.Rank = currentRank;
                currentRank++;
            });
        }
    }
}
