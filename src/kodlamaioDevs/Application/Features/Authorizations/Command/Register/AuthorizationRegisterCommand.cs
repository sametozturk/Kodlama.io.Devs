using Application.Features.Authorizations.Dtos;
using Application.Features.Authorizations.Rules;
using Application.Services.AuthService;
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
    public class AuthorizationRegisterCommand : IRequest<RegisteredDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAdress { get; set; }

        public class AuthorizationRegisterCommandHandler : IRequestHandler<AuthorizationRegisterCommand, RegisteredDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthorizationsBusinessRules _authorizationsBusiness;
            private readonly IAuthService _authService;

            public AuthorizationRegisterCommandHandler(IUserRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, ITokenHelper tokenHelper, AuthorizationsBusinessRules authorizationsBusinessRules, IAuthService authService)
            {
                _userRepository = userRepository;
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _authorizationsBusiness = authorizationsBusinessRules;
                _authService = authService;
            }

            public async Task<RegisteredDto> Handle(AuthorizationRegisterCommand request, CancellationToken cancellationToken)
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
                User createdUser = await _userRepository.AddAsync(user);

                var result = await _userOperationClaimRepository.AddAsync(new UserOperationClaim() { UserId = createdUser.Id, OperationClaimId = 1 });

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAdress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                RegisteredDto registeredDto = new()
                {
                    RefreshToken = addedRefreshToken,
                    AccessToken = createdAccessToken
                };
                return registeredDto;




            }
        }
    }
}
