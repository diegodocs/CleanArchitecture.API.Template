using System.Threading.Tasks;
using Api.Template.Domain.Commands.Funds;
using Api.Template.Domain.Models;
using Api.Common.Cqrs.Core.Bus;
using Api.Common.Cqrs.Core.CommandHandlers;
using Api.Common.Repository.Contracts.Core.Repository;

namespace Api.Template.Domain.CommandHandlers.Funds
{
    public class UpdateFundCommandHandler :
        CommandHandler,
        ICommandHandlerTwoWay<UpdateFundCommand, Fund>
    {
        private readonly IRepository<Fund> fundRepository;

        public UpdateFundCommandHandler(IRepository<Fund> fundRepository, IMessageBus bus) : base(bus)
        {
            this.fundRepository = fundRepository;
        }

        public Task<Fund> Handle(UpdateFundCommand command)
        {
            return Task.Run(() =>
            {
                //Domain Changes
                var fund = fundRepository.FindById(command.Id);
                fund.Name = command.Name;
                fund.Description = command.Description;
                fund.AuditUserId = command.AuditUserId;                

                //Persistence
                fundRepository.Update(fund);

                return fund;
            });
        }
    }
}