using System;
using System.ComponentModel.DataAnnotations;
using Api.Common.Cqrs.Core.Commands;

namespace Api.Template.Domain.Commands.Funds
{
    public class DeleteFundCommand : Command
    {
        public DeleteFundCommand(Guid id)
        {
            Id = id;
        }

        [Required]
        public Guid Id { get; set; }
    }
}