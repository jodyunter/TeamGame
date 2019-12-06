using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Service.Models
{
    public class TeamViewModel:IViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Skill { get; set; }
    }
}
