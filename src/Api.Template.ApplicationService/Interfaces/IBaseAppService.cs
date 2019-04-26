using AutoMapper;
using Api.Common.Cqrs.Core.Bus;
using Api.Common.WebServer.Server;

namespace Api.Template.ApplicationService.Interfaces
{
    public interface IBaseAppService
    {
        IMessageBus MessageBus { get; set; }
        UserContext CurrentUser { get; set; }
        IMapper Mapper { get; set; }
    }
}
