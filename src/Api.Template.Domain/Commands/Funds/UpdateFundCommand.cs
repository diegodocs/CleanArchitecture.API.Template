using System;
using System.ComponentModel.DataAnnotations;
using Api.Common.Cqrs.Core.Commands;

namespace Api.Template.Domain.Commands.Funds
{
    public class UpdateFundCommand : Command
    {
        public UpdateFundCommand(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        [Required] public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Name { get; set; }

        [MinLength(2)]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}