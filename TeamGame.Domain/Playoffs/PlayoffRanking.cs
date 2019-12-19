using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Playoffs
{
    public class PlayoffRanking:IRanking
    {
        public int Rank { get; set; }        
        public string Group { get; set; }

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
    }
}
