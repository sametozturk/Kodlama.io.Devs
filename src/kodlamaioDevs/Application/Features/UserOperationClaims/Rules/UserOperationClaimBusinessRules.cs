using Application.Features.UserOperationClaims.Dtos;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOperationClaimRepository _operationClaimRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository, IUserRepository userRepository, IOperationClaimRepository operationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _userRepository = userRepository;
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task UserOperationClaimCanNotBeDuplicatedWhenInserted(UserOperationClaim userOperationClaim)
        {
            UserOperationClaim? result = await _userOperationClaimRepository.GetAsync(b => b.UserId== userOperationClaim.UserId && b.OperationClaimId== userOperationClaim.OperationClaimId);
            if (result!=null) throw new BusinessException("User Operation Claim exists.");
        }
        public async Task UserShouldExistWhenRequested(int id)
        {
            User? result= await _userRepository.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException("User does not exist.");
        }

        public async Task OperationClaimShouldExistWhenRequested(int id)
        {
            OperationClaim? result = await _operationClaimRepository.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException("Operation Claim does not exist.");
        }
        public void UserOperationClaimShouldExistWhenRequested(UserOperationClaim userOperationClaim)
        {
            if (userOperationClaim == null) throw new BusinessException("Requested user operation claim does not exist.");
        }
    }
}
