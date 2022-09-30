using Application.Features.GitHubProfiles.Models;
using Application.Features.GitHubProfiles.Queries.GetListGitHubProfile;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Queries.GetListGitHubProfileDynamic
{
    public class GetListGitHubProfileDynamicQuery : IRequest<GitHubProfileListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListGitHubProfileDynamicQueryHandler : IRequestHandler<GetListGitHubProfileDynamicQuery, GitHubProfileListModel>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IMapper _mapper;

            public GetListGitHubProfileDynamicQueryHandler(IGitHubProfileRepository gitHubProfileRepository, IMapper mapper)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _mapper = mapper;
            }

            public async Task<GitHubProfileListModel> Handle(GetListGitHubProfileDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<GitHubProfile> gitHubProfile = await _gitHubProfileRepository.GetListByDynamicAsync(dynamic: request.Dynamic, include: g => g.Include(u => u.User), index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                GitHubProfileListModel gitHubProfileListModel = _mapper.Map<GitHubProfileListModel>(gitHubProfile);

                return gitHubProfileListModel;

            }
        }
    }
}
