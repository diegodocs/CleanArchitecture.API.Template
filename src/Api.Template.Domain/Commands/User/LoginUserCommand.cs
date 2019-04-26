using Api.Common.Cqrs.Core.Commands;
using System.ComponentModel.DataAnnotations;

namespace Api.Template.Domain.Commands.User
{
    public class LoginUserCommand : Command
    {
        public LoginUserCommand(string name, string login, string email, string sub, bool isPreparer, bool isApprover)
        {
            Name = name;
            Login = login;            
            Email = email;
            Sub = sub;
            IsPreparer = isPreparer;
            IsApprover = isApprover;
        }

        public string Name { get; set; }

        [Required]
        public string Login { get; protected set; }        

        [Required]
        public string Email { get; protected set; }

        //[Required]
        public string Sub { get; set; }

        //[Required]
        public bool IsPreparer { get; set; }

        //[Required]
        public bool IsApprover { get; set; }
    }
}