using Application.Features.Languages.Commands.DeleteLanguage;
using Application.Features.Languages.Commands.UpdateLanguage;
using Application.Features.Languages.Dtos;
using Application.Features.Technologies.Command.CreateTechnology;
using Application.Features.Technologies.Command.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();

            CreateMap<Technology, TechnologyListDto>().ForMember(c => c.LanguageName, opt => opt.MapFrom(c=>c.Language.Name)).ReverseMap();

            CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();


            CreateMap<Technology, UpdatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, UpdateTechnologyCommand>().ReverseMap();

            CreateMap<Technology, DeletedTechnologyDto>().ReverseMap();
            CreateMap<Technology, DeleteLanguageCommand>().ReverseMap();
        }
    }
}
