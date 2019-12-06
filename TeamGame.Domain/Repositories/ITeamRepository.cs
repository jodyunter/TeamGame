using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Domain.Repositories
{
    public interface ITeamRepository
    {
        Team Get(long id);
        IList<Team> GetAll();
        IList<Team> GetByStatus(bool status);
        Team GetByName(string name);
        Team Update(Team team);
        void Delete(long id);
        Team Create(Team team);

    }
}
