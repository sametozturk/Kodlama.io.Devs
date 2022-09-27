using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Rules
{
    public class AuthorizationsBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthorizationsBusinessRules(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }
        public async Task UserEmailCanNotBeDuplicatedWhenInserted(string email)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == email);
            if (user != null) throw new BusinessException("Email already exists.");
        }

        public async Task UserShouldBeExists(User? user)
        {
            if (user == null) throw new BusinessException("User does not exists.");
        }

        public async Task UserPasswordShouldBeMatch(int id, string password)
        {
            User? user = await _userRepository.GetAsync(u => u.Id == id);
            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new BusinessException("Password don't match.");

        }
    }
}
