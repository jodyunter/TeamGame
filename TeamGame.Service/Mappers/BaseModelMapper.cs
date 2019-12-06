using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamGame.Domain;
using TeamGame.Service.Models;

namespace TeamGame.Service.Mappers
{
    public abstract class BaseModelMapper : IModelMapper
    {        

        public IList<Message> Messages { get; set; }

        public abstract IViewModel DomainToModel(IDomain domain);        
        public abstract IDomain ModelToDomain(IViewModel model);

        public IList<IDomain> ModelToDomain(IList<IViewModel> models)
        {
            var list = new List<IDomain>();

            models.ToList().ForEach(model =>
            {
                list.Add(ModelToDomain(model));
            });

            return list;
        }

        public IList<IViewModel> DomainToModel(IList<IDomain> domains)
        {
            var list = new List<IViewModel>();

            domains.ToList().ForEach(domain =>
            {
                list.Add(DomainToModel(domain));
            });

            return list;
        }

    }
}
