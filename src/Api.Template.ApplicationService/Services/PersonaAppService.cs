using Api.Common.Cqrs.Core.Bus;
using Api.Common.Repository.Contracts.Core.Repository;
using Api.Template.ApplicationService.Interfaces;
using Api.Template.ApplicationService.ViewModels;
using Api.Template.Domain.Commands.Personas;
using Api.Template.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Template.ApplicationService.Services
{
    public class PersonaAppService : BaseAppService, IPersonaAppService
    {
        private readonly IMapper mapper;
        private readonly IMessageBus messageBus;
        private readonly IRepository<Persona> repository;

        public PersonaAppService(
            IHttpContextAccessor contextAccessor,
            IMessageBus messageBus,
            IMapper mapper,
            IRepository<Persona> repository) : base(contextAccessor)
        {
            this.messageBus = messageBus;
            this.mapper = mapper;
            this.repository = repository;
        }

        public IEnumerable<PersonaViewModel> GetAll()
        {
            return mapper.Map<IEnumerable<PersonaViewModel>>(repository.All()).OrderBy(x => x.Name);
        }

        public PersonaViewModel Get(Guid id)
        {
            return mapper.Map<PersonaViewModel>(repository.FindById(id));
        }

        public IEnumerable<PersonaViewModel> GetListByName(string name)
        {
            return
                mapper.Map<IEnumerable<PersonaViewModel>>(
                        repository
                            .FindList(x => x.Name.Contains(name)))
                    .OrderBy(x => x.Name);
        }

        public PersonaViewModel GetById(Guid id)
        {
            return mapper.Map<PersonaViewModel>(repository.FindById(id));
        }

        public PersonaViewModel Create(CreatePersonaCommand command)
        {
            return mapper
                .Map<PersonaViewModel>(
                    messageBus
                        .DispatchCommandTwoWay<CreatePersonaCommand, Persona
                        >(command).Result);
        }

        public PersonaViewModel Update(UpdatePersonaCommand command)
        {
            return mapper
                .Map<PersonaViewModel>(
                    messageBus
                        .DispatchCommandTwoWay<UpdatePersonaCommand, Persona
                        >(command).Result);
        }

        public void Delete(DeletePersonaCommand commandDelete)
        {
            messageBus.DispatchCommand(commandDelete);
        }
    }
}