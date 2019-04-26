using Api.Common.Repository.Contracts.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Template.Domain.Models
{
    public class User : DomainEntity
    {
        [MinLength(2)]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [MinLength(2)]
        [MaxLength(255)]
        [Required]
        public string Login { get; set; }

        [MinLength(2)]
        [MaxLength(255)]
        public string Email { get; set; }
        
        public DateTime? LastLoginDate { get; set; }

        public bool IsApprover { get; set; }
    }
}