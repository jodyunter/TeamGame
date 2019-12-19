using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamGame.Domain.Playoffs
{
    public class PlayoffGroup
    {
        public string Name { get; set; }
        public IList<PlayoffRanking> Rankings { get; set; } = new List<PlayoffRanking>();

        public PlayoffTeam GetTeamByRank(int rank)
        {
            return (PlayoffTeam)Rankings.Where(r => r.Rank == rank).First().Team;
        }
    }
}
