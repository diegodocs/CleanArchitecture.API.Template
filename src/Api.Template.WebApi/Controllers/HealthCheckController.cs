using Api.Template.ApplicationService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Template.CI.WebApi.Controllers
{
    public class HealthCheckController : BaseController
    {
        private readonly IReleaseCallStatusAppService appService;
        private readonly ILogger<HealthCheckController> logger;

        public HealthCheckController(IReleaseCallStatusAppService appService,
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
                    appService.GetAll();

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