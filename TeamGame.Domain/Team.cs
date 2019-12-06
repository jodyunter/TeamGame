using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain
{
    public class Team:DataObject, ITeam, IDomain
    {
        public string Name { get; set; }
        public int Skill { get; set; }
    }
}
