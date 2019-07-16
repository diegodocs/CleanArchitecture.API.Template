using Api.Common.Cqrs.Core.Commands;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Template.Domain.Commands.ReleaseCallsStatus
{
    public class UpdateReleaseCallStatusCommand : Command
    {
        public UpdateReleaseCallStatusCommand(Guid id, string name)
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