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
