using System.ComponentModel.DataAnnotations;
using Api.Common.Cqrs.Core.Commands;

namespace Api.Template.Domain.Commands.Funds
{
    public class CreateFundCommand : Command
    {
        public CreateFundCommand(string name, string description, string legalName)
        {
            Name = name;
            Description = description;            
            LegalName = legalName;            
        }

        [MinLength(2)]
        [MaxLength(255)]
        [Required]
        public string Name { get; protected set; }

        [MinLength(2)]
        [MaxLength(255)]
        public string Description { get; protected set; }
                
        [MinLength(2)]
        [MaxLength(255)]
        [Required]
        public string LegalName { get; set; }
    }
}