using Api.Template.ApplicationService.Interfaces;
using Api.Template.Domain.Commands.Funds;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Template.WebApi.Controllers
{
    public class FundController : BaseAuthorizedController
    {
        private readonly IFundAppService fundAppService;

        public FundController(IFundAppService fundAppService)
        {
            this.fundAppService = fundAppService;            
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(fundAppService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(fundAppService.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateFundCommand command)
        {
            return Ok(fundAppService.Create(command));            
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] Guid id, [FromBody] UpdateFundCommand command)
        {
            return Ok(fundAppService.Update(command));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            fundAppService.Delete(new DeleteFundCommand(id));
            return Ok();
        }
    }
}