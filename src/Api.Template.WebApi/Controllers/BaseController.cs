using Microsoft.AspNetCore.Mvc;

namespace Api.Template.CI.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class BaseController : Controller
    {
    }
}