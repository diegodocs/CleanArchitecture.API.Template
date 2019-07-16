using Api.Common.Repository.Contracts.Core.Entities;
using System;

namespace Api.Template.Domain.Models
{
    public class User : DomainEntity
    {
        public Guid UserId { get; set; }
        public int SecurityUserId { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Pwd { get; set; }
        public string ConfirmationPassword { get; set; }
        public string OriginSystem { get; set; }
        public DateTime DateInclusion { get; set; }
        public string Login { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}