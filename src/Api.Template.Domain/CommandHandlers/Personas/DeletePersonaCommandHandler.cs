using Api.Common.Cqrs.Core.CommandHandlers;
using Api.Common.Repository.Contracts.Core.Repository;
using Api.Template.Domain.Commands.Personas;
using Api.Template.Domain.Models;
using System.Threading.Tasks;

namespace Api.Template.Domain.CommandHandlers.Personas
{
    public class DeletePersonaCommandHandler :
        ICommandHandler<DeletePersonaCommand>
    {
        private readonly IRepository<Persona> repository;

        public DeletePersonaCommandHandler(IRepository<Persona> repository)
        {
            this.repository = repository;
        }

        public Task Handle(DeletePersonaCommand command)
        {
            //Persistence
            repository.Delete(command.Id);

            return Task.CompletedTask;
        }
    }
}