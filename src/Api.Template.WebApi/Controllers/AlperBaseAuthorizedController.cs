using Microsoft.AspNetCore.Authorization;

namespace Api.Template.CI.WebApi.Controllers
{
    [Authorize("Bearer")]
    public class BaseAuthorizedController : BaseController
    {
    }
}