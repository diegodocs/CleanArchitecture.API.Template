using Api.Common.Cqrs.Core.CommandHandlers;
using Api.Common.Repository.Contracts.Core.Repository;
using Api.Template.Domain.Commands.Personas;
using Api.Template.Domain.Models;
using System.Threading.Tasks;

namespace Api.Template.Domain.CommandHandlers.Personas
{
    public class UpdatePersonaCommandHandler :
        ICommandHandlerTwoWay<UpdatePersonaCommand, Persona>
    {
        private readonly IRepository<Persona> repository;

        public UpdatePersonaCommandHandler(IRepository<Persona> repository)
        {
            this.repository = repository;
        }

        public Task<Persona> Handle(UpdatePersonaCommand command)
        {
            return Task.Run(() =>
            {
                //Domain Changes
                var instance = repository.FindById(command.Id);
                instance.Name = command.Name;

                //Persistence
                repository.Update(instance);

                return instance;
            });
        }
    }
}