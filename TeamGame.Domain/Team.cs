using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain
{
    public class Team:IDataObject, ITeam, IDomain
    {
        public long Id { get;  set; }
        public string Name { get; set; }
        public int Skill { get; set; }

        public Team() { }
        public Team(long id, string name, int skill)
        {
            Id = id;
            Name = name;
            Skill = skill;
        }
    }
}
