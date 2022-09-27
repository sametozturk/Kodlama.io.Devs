using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Command.CreateTechnology
{
    public class CreateTechnologyCommand:IRequest<CreatedTechnologyDto>
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public class CreateTechnologyCommandHandler:IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);
                Technology mappedTechnology = _mapper.Map<Technology>(request);
                await _technologyBusinessRules.LanguageShouldExistWhenTechnologyRequested(mappedTechnology);
                Technology createdTechnology = await _technologyRepository.AddAsync(mappedTechnology);
                CreatedTechnologyDto createdBrandDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);
                return createdBrandDto;
            }
        }
    }
}
