using System;
using System.ComponentModel.DataAnnotations;
using Api.Common.Cqrs.Core.Commands;

namespace Api.Template.Domain.Commands.Funds
{
    public class UpdateFundCommand : Command
    {
        public UpdateFundCommand(Guid id, string name, string description, string legalName)
        {
            Id = id;
            Name = name;
            Description = description;
            LegalName = legalName;
        }

        [MinLength(2)]
        [MaxLength(255)]
        [Required]
        public string LegalName { get; protected set; }

        [Required] public Guid Id { get; protected set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Name { get; protected set; }

        [MinLength(2)]
        [MaxLength(255)]
        public string Description { get; protected set; }
    }
}