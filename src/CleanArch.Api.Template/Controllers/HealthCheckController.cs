using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Template.WebApi.Controllers
{    
    public class HealthCheckController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return Ok("Status OK");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new {ex.Message});
                }
            });
        }
    }
}