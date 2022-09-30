﻿using Application.Features.Technologies.Models;
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

namespace Application.Features.Technologies.Queries.GetListTechnologyDynamic
{
    public class GetListTechnologyDynamicQuery:IRequest<TechnologyListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListTechnologyDynamicQueryHandler:IRequestHandler<GetListTechnologyDynamicQuery,TechnologyListModel>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;

            public GetListTechnologyDynamicQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<TechnologyListModel> Handle(GetListTechnologyDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Technology> technologies= await _technologyRepository.GetListByDynamicAsync(dynamic: request.Dynamic, include: t => t.Include(l => l.Language), index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                TechnologyListModel technologyListModel = _mapper.Map<TechnologyListModel>(technologies);

                return technologyListModel;
            }
        }
    }
}
