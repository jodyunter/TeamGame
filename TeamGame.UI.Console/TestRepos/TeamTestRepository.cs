using System;
using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain;
using TeamGame.Domain.Repositories;

namespace TeamGame.UI.ConsoleApp.TestRepos
{
    public class TeamTestRepository : ITeamRepository
    {
        public IList<Team> Data = new List<Team>()
        {
            new Team(1, "Team 1", 5),
            new Team(2, "Team 2", 5),
            new Team(3, "Team 3", 5),
            new Team(4, "Team 4", 5),
            new Team(5, "Team 5", 5),
            new Team(6, "Team 6", 5),
            new Team(7, "Team 7", 5),
            new Team(8, "Team 8", 5),
            new Team(9, "Team 9", 5),
            new Team(10, "Team 10", 5),
            new Team(11, "Team 11", 5),
            new Team(12, "Team 12", 5)
        };
        public Team Create(Team team)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Team Get(long id)
        {
            return Data.ToList().Where(t => t.Id == id).FirstOrDefault();
        }

        public IList<Team> GetAll()
        {
            return Data;
        }

        public Team GetByName(string name)
        {
            return Data.ToList().Where(t => t.Name.Equals(name)).FirstOrDefault();
        }

        public IList<Team> GetByStatus(bool status)
        {
            throw new NotImplementedException();
        }

        public Team Update(Team team)
        {
            throw new NotImplementedException();
        }
    }
}
