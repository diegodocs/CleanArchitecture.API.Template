using Api.Common.Repository.Contracts.Core.Entities;
using System;

namespace Api.Template.ApplicationService.ViewModels
{
    public class UserViewModel : IApplicationViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public DateTime LastLoginDate { get; set; }

        public bool IsApprover { get; set; }

        public bool IsPreparer { get; set; }
    }
}