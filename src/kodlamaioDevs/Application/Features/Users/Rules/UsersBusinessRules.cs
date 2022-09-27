using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules
{
    public class UsersBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public UsersBusinessRules(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }
        public async Task UserEmailShouldBeNotExists(string email)
        {
            User user = await _userRepository.GetAsync(u => u.Email == email);
            if (user != null) throw new BusinessException("Email name exists.");
        }

        public Task UserShouldBeExists(User user)
        {
            if (user == null) throw new BusinessException("User does not exists.");
            return Task.CompletedTask;
        }
    }
}
