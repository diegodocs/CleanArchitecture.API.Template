using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.Personas;
using System;
using System.Collections.Generic;

namespace Api.Template.ApplicationService.Interfaces
{
    public interface IPersonaAppService
    {
        IEnumerable<PersonaViewModel> GetAll();

        void Delete(DeletePersonaCommand commandDelete);

        PersonaViewModel Get(Guid id);

        IEnumerable<PersonaViewModel> GetListByName(string name);

        PersonaViewModel Create(CreatePersonaCommand command);

        PersonaViewModel Update(UpdatePersonaCommand command);
    }
}