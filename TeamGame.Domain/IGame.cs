using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain
{
    public interface IGame
    {
        ITeam Home { get; set; }
        ITeam Away { get; set; }
        int HomeScore { get; set; }
        int AwayScore { get; set; }
        bool Complete { get; set; }
        bool Processed { get; set; }


        public void Play(Random random);

    }
}
