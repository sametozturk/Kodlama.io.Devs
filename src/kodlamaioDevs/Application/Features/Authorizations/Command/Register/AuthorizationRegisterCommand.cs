using Application.Features.Authorizations.Dtos;
using Application.Features.Authorizations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Command.Register
{
    public class AuthorizationRegisterCommand : IRequest<AuthRegisterDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }

        public class AuthorizationRegisterCommandHandler : IRequestHandler<AuthorizationRegisterCommand, AuthRegisterDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthorizationsBusinessRules _authorizationsBusiness;

            public AuthorizationRegisterCommandHandler(IUserRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, ITokenHelper tokenHelper, AuthorizationsBusinessRules authorizationsBusinessRules)
            {
                _userRepository = userRepository;
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _authorizationsBusiness = authorizationsBusinessRules;
            }

            public async Task<AuthRegisterDto> Handle(AuthorizationRegisterCommand request, CancellationToken cancellationToken)
            {
                await _authorizationsBusiness.UserEmailCanNotBeDuplicatedWhenInserted(request.UserForRegisterDto.Email);
                Byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

                User user = new User
                {
                    Email = request.UserForRegisterDto.Email,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };
                User registeredUser = await _userRepository.AddAsync(user);

                var result = await _userOperationClaimRepository.AddAsync(new UserOperationClaim() { UserId = registeredUser.Id, OperationClaimId = 1 });
                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id, include: u => u.Include(u => u.OperationClaim));
                IList<OperationClaim> operationClaims = userOperationClaims.Items.Select(u => new OperationClaim { Id = u.OperationClaim.Id, Name = u.OperationClaim.Name }).ToList();
                AccessToken accessToken = _tokenHelper.CreateToken(registeredUser, userOperationClaims.Items.Select(u => u.OperationClaim).ToList());

                return new() { AccessToken = accessToken };




            }
        }
    }
}
