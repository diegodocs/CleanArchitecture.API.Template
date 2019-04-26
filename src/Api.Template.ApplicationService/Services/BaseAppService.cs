using AutoMapper;
using Api.Template.ApplicationService.Interfaces;
using Api.Common.Cqrs.Core.Bus;
using Api.Common.WebServer.Server;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Api.Template.ApplicationService.Services
{
    public class BaseAppService : IBaseAppService
    {
        public BaseAppService(IHttpContextAccessor contextAccessor, IMessageBus bus, IMapper mapper)
        {
            this.MessageBus = bus;
            this.Mapper = mapper;

            if (contextAccessor.HttpContext != null)
            {
                var headerUserId = contextAccessor.HttpContext.Request.Headers["UserId"].FirstOrDefault();

                if (headerUserId != null)
                {
                    CurrentUser = new UserContext { Id = Guid.Parse(headerUserId.ToString()) };
                }
            }
        }

        public IMessageBus MessageBus { get; set; }
        public UserContext CurrentUser { get; set; }
        public IMapper Mapper { get; set; }
    }
}