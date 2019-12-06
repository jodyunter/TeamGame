using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain
{
    public interface ITeam: IDomain
    {                
        string Name { get; set; }
        int Skill { get; set; }
    }
}
