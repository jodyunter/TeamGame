using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Service.Models;

namespace TeamGame.Service
{
    public interface ITeamService
    {
        IList<IViewModel> GetAll();
        IViewModel GetByName(string name);
    }
}
