using System;
using System.Collections.Generic;
using System.Linq;
using TeamGame.Domain;
using TeamGame.Domain.Repositories;
using TeamGame.Service.Mappers;
using TeamGame.Service.Models;

namespace TeamGame.Service
{
    public class TeamService : ITeamService
    {
        private ITeamRepository teamRepo;
        private IModelMapper mapper;

        private IList<TeamViewModel> teamViewList = new List<TeamViewModel>()
            {
                new TeamViewModel() { Id = 1, Name = "Team 1", Skill = 5 },
                new TeamViewModel() { Id = 2, Name = "Team 2", Skill = 5 },
                new TeamViewModel() { Id = 3, Name = "Team 3", Skill = 5 },
                new TeamViewModel() { Id = 4, Name = "Team 4", Skill = 5 },
                new TeamViewModel() { Id = 5, Name = "Team 5", Skill = 5 },
                new TeamViewModel() { Id = 6, Name = "Team 6", Skill = 5 },
            };

        public TeamService(ITeamRepository teamRepository, IModelMapper modelMapper)
        {
            teamRepo = teamRepository;
            mapper = modelMapper;
        }

        public IList<IViewModel> GetAll()
        {
            return mapper.DomainToModel(teamRepo.GetAll().ToList<IDomain>());


        }

        public IViewModel GetByName(string name)
        {
            return mapper.DomainToModel(teamRepo.GetByName(name));
        }
    }
}
