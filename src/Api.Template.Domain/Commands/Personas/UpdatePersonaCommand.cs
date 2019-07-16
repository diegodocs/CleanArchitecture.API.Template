using Api.Common.Cqrs.Core.Commands;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Template.Domain.Commands.Personas
{
    public class UpdatePersonaCommand : Command
    {
        public UpdatePersonaCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        [MinLength(2)]
        [MaxLength(255)]
        [Required]
        public string Name { get; protected set; }

        [Required] public Guid Id { get; protected set; }
    }
}