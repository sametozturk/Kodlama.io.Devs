using Application.Features.UserOperationClaims.Command.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using Application.Features.Users.Rules;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, CreatedUserOperationClaimDto>().ReverseMap();

            CreateMap<UserOperationClaim, DeletedUserOperationClaimDto>().ReverseMap();

            CreateMap<IPaginate<UserOperationClaim>, UserOperationClaimListModel>().ReverseMap();
            CreateMap<UserOperationClaim, UserOperationClaimListDto>()
                .ForMember(x=>x.UserName,opt=>opt.MapFrom(x=>x.User.FirstName))
                .ForMember(x=>x.UserSurname,opt=>opt.MapFrom(x=>x.User.LastName))
                .ForMember(x=>x.UserMail,opt=>opt.MapFrom(x=>x.User.Email))
                .ForMember(x=>x.OperationClaimName,opt=>opt.MapFrom(x=>x.OperationClaim.Name)).ReverseMap();


        }
    }
}
