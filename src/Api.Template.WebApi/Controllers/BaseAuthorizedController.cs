using Api.Template.CI.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace Api.Template.WebApi.Controllers
{
    [Authorize("Bearer")]
    public class BaseAuthorizedController : BaseController
    {
    }
}