using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain;
using TeamGame.Service.Models;

namespace TeamGame.Service.Mappers
{
    public class TeamModelMapper : BaseModelMapper
    {        
        public override IViewModel DomainToModel(IDomain domain)
        {
            var team = (Team)domain;

            var model = new TeamViewModel()
            {
                Id = team.Id,
                Name = team.Name,
                Skill = team.Skill
            };

            return model;
        }


        public override IDomain ModelToDomain(IViewModel model)
        {
            var teamModel = (TeamViewModel)model;

            var team = new Team()
            {
                Id = teamModel.Id,
                Name = teamModel.Name,
                Skill = teamModel.Skill
            };

            return team;
        }

    }
}
