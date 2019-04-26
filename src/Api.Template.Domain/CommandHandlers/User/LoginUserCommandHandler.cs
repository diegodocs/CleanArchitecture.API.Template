using Api.Common.Cqrs.Core.Bus;
using Api.Common.Cqrs.Core.CommandHandlers;
using Api.Common.Repository.Contracts.Core.Repository;
using Api.Template.Domain.Commands.User;
using Api.Template.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Api.Template.Domain.CommandHandlers.Reconciliations
{
    public class LoginUserCommandHandler : CommandHandler,
        ICommandHandlerTwoWay<LoginUserCommand, User>
    {
        private readonly IRepository<User> userRepository;

        public LoginUserCommandHandler(
            IRepository<User> userRepository,
            IMessageBus bus) : base(bus)
        {
            this.userRepository = userRepository;
        }

        public Task<User> Handle(LoginUserCommand command)
        {
            return Task.Run(() =>
            {
                var user = new User
                {
                    Name = command.Name,
                    Login = command.Login.ToLower(), 
                    Email = command.Email.ToLower(),
                    LastLoginDate = DateTime.UtcNow,
                    IsApprover = command.IsApprover,                    
                    IsActive = true
                };

                var existentUser = userRepository.Find(x => x.Email == command.Email && x.Login == command.Login && x.IsActive);

                if (existentUser == null)
                {
                    userRepository.Insert(user);
                    return user;
                }
                else
                {
                    existentUser.Name = command.Name;
                    existentUser.IsApprover = command.IsApprover;                    
                    userRepository.Update(existentUser);
                    return existentUser;
                }
                   
            });
        }
    }
}