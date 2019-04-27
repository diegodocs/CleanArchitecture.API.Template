using Api.Template.ApplicationService.Interfaces;
using Api.Template.Domain.Commands.User;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Template.WebApi.Controllers
{
    public class UserController : BaseAuthorizedController
    {        
        private readonly IUserAppService userAppService;

        public UserController(IUserAppService userAppService)
        {            
            this.userAppService = userAppService;
        }        

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserCommand command)
        {
            var loggedUser = userAppService.Login(command);
            if (loggedUser == null)
                return Forbid();

            return Ok(loggedUser);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(userAppService.GetById(id));
        }
    }
}