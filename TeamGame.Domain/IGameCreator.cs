using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain
{
    public interface IGameCreator
    {
        public Game CreateGame(ITeam home, ITeam away); //need game rules too?
    }
}
