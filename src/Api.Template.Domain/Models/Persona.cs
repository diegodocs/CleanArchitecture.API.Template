using Api.Common.Repository.Contracts.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Api.Template.Domain.Models
{
    public class Persona : DomainEntity
    {
        [MinLength(2)]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
    }
}