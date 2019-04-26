using Api.Common.Repository.Contracts.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Api.Template.Domain.Models
{
    public class Fund : DomainEntity
    {
        [MinLength(2)]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [MinLength(2)]
        [MaxLength(255)]
        public string Description { get; set; }
        
        [MinLength(2)]
        [MaxLength(255)]
        [Required]
        public string LegalName { get; set; }
    }
}
