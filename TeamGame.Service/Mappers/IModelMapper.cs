using System;
using System.Collections.Generic;
using System.Text;
using TeamGame.Domain;
using TeamGame.Service.Models;

namespace TeamGame.Service.Mappers
{
    public interface IModelMapper
    {
        IDomain ModelToDomain(IViewModel model);        
        IViewModel DomainToModel(IDomain domain);        
        IList<Message> Messages { get; set; }

        IList<IDomain> ModelToDomain(IList<IViewModel> list);
        IList<IViewModel> DomainToModel(IList<IDomain> list);
    }
}
