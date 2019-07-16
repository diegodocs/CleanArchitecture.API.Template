using Api.Common.Cqrs.Core.Commands;
using System.ComponentModel.DataAnnotations;

namespace Api.Template.Domain.Commands.ReleaseCallsStatus
{
    public class CreateReleaseCallStatusCommand : Command
    {
        public CreateReleaseCallStatusCommand(string name)
        {
            Name = name;
        }

        [MinLength(2)]
        [MaxLength(255)]
        [Required]
        public string Name { get; protected set; }
    }
}