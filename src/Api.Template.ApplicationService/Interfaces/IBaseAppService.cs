using Api.Common.WebServer.Server;

namespace Api.Template.ApplicationService.Interfaces
{
    public interface IBaseAppService
    {
        UserContext CurrentUser { get; set; }
    }
}