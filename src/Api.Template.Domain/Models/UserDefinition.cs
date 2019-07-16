using System;

namespace Api.Template.Domain.Models
{
    public class UserDefinition
    {
        public User SystemAppUser =>
            new User
            {
                Id = new Guid("9ae42a23-1e01-4402-bc3f-c74a8ee295c6"),
                CreateDate = new DateTime(2018, 08, 01),
                IsActive = true,
                Name = "System-App-User",
                Login = "system-app-user",
                Email = "system-app-user@test.com.br",
                AuditUserId = new Guid("9ae42a23-1e01-4402-bc3f-c74a8ee295c6")
            };
    }
}