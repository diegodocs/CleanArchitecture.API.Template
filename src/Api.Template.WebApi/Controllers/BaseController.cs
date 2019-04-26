using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Template.WebApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class BaseController : Controller
    {        
    }
}