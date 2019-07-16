using Api.Common.Cqrs.Core.Commands;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Template.Domain.Commands.Personas
{
    public class DeletePersonaCommand : Command
    {
        public DeletePersonaCommand(Guid id)
        {
            Id = id;
        }

        [Required] public Guid Id { get; protected set; }
    }
}