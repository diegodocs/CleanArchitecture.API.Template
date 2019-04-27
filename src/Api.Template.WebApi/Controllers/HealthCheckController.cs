using System;
using System.Net;
using System.Threading.Tasks;
using Api.Template.ApplicationService.Interfaces;
using Api.Template.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Template.WebApi.Controllers
{    
    public class HealthCheckController : BaseController
    {
        private readonly IUserAppService appService;
        private readonly ILogger<HealthCheckController> logger;

        public HealthCheckController(IUserAppService appService,
            ILogger<HealthCheckController> logger)
        {
            this.appService = appService;
            this.logger = logger;
        }

        /// <summary>
        /// Verify system's conectivity
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.Run(() =>
            {
                try
                {
                    const string message = "HealthCheck Status OK";
                    //validate Database Connection (read method)
                    User user = new UserDefinition().SystemAppUser;
                    appService.GetById(user.Id);

                    logger.LogInformation(message);

                    return Ok(message);
                }
                catch (Exception ex)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { ex.Message });
                }
            });
        }
    }
}