using Application.Features.GitHubProfiles.Command.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Command.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Command.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Models;
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

namespace Application.Features.GitHubProfiles.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<GitHubProfile, CreatedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, CreateGitHubProfileCommand>().ReverseMap();

            CreateMap<GitHubProfile, DeletedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, DeleteGitHubProfileCommand>().ReverseMap();

            CreateMap<GitHubProfile, UpdatedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, UpdateGitHubProfileCommand>().ReverseMap();

            CreateMap<GitHubProfile, GitHubProfileListDto>().ForMember(c => c.UserEmail, opt => opt.MapFrom(c => c.User.Email)).ReverseMap();
            CreateMap<IPaginate<GitHubProfile>, GitHubProfileListModel>().ReverseMap();
        }
    }
}
