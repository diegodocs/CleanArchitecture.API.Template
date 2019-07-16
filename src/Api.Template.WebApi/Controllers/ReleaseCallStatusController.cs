using Api.Template.ApplicationService.Interfaces;
using Api.Template.Domain.Commands.ReleaseCallsStatus;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Template.CI.WebApi.Controllers
{
    public class ReleaseCallStatusController : BaseController
    {
        private readonly IReleaseCallStatusAppService appService;

        public ReleaseCallStatusController(IReleaseCallStatusAppService appService)
        {
            this.appService = appService;
        }

        /// <summary>
        /// Get Status List of Release Call
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(appService.GetAll());
        }

        /// <summary>
        /// Get Status List of Release Call By ID
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            return Ok(appService.Get(id));
        }

        /// <summary>
        /// Get Status List of Release Call By Name
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet("name/{name}")]
        public IActionResult Get([FromRoute] string name)
        {
            return Ok(appService.GetListByName(name));
        }

        /// <summary>
        /// Post Status of Release Call
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] CreateReleaseCallStatusCommand command)
        {
            return Ok(appService.Create(command));
        }

        /// <summary>
        /// Delete a Status of Release Call
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            appService.Delete(new DeleteReleaseCallStatusCommand(id));
            return Ok();
        }

        /// <summary>
        /// Put Status of Release Call
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateReleaseCallStatusCommand command)
        {
            return Ok(appService.Update(command));
        }
    }
}