using System.ComponentModel.DataAnnotations;
using Api.Common.Cqrs.Core.Commands;

namespace Api.Template.Domain.Commands.Funds
{
    public class CreateFundCommand : Command
    {
        public CreateFundCommand(string name, string description)
        {
            Name = name;
            Description = description;             
        }

        [MinLength(2)]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [MinLength(2)]
        [MaxLength(255)]
        public string Description { get; set; }
                
    }
}