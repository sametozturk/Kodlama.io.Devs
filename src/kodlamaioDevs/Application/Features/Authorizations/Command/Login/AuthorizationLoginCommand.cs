using Application.Features.Authorizations.Dtos;
using Application.Features.Authorizations.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Command.Login
{
    public class AuthorizationLoginCommand:IRequest<AuthLoginDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        

        public class AuthorizationLoginCommandHandler:IRequestHandler<AuthorizationLoginCommand, AuthLoginDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthorizationsBusinessRules _authorizationsBusinessRules;

            public AuthorizationLoginCommandHandler(IUserRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper, AuthorizationsBusinessRules authorizationsBusinessRules)
            {
                _userRepository = userRepository;
                _userOperationClaimRepository = userOperationClaimRepository;
                _tokenHelper = tokenHelper;
                _authorizationsBusinessRules = authorizationsBusinessRules;
            }

            public async Task<AuthLoginDto> Handle(AuthorizationLoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(x => x.Email == request.UserForLoginDto.Email);

                await _authorizationsBusinessRules.UserShouldBeExists(user);
                await _authorizationsBusinessRules.UserPasswordShouldBeMatch(user.Id, request.UserForLoginDto.Password);

                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id, include: u => u.Include(u => u.OperationClaim));

                AccessToken accessToken = _tokenHelper.CreateToken(user, userOperationClaims.Items.Select(u => u.OperationClaim).ToList());
                RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user,Dns.GetHostByName(Dns.GetHostName()).AddressList[1].ToString());  
                //refresh token tabloya insert atılcak?

                AuthLoginDto authLoginDto = new()
                {
                    AccessToken = accessToken,
                    //  ??RefreshToken = refreshToken??
                };
                return authLoginDto;
            }
        }
}
}
