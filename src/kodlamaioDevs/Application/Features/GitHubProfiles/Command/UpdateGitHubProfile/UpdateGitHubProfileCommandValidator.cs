using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Command.UpdateGitHubProfile
{
    public class UpdateGitHubProfileCommandValidator : AbstractValidator<UpdateGitHubProfileCommand>
    {
        public UpdateGitHubProfileCommandValidator()
        {
            RuleFor(g => g.GitHubUrl).NotEmpty();
            RuleFor(g => g.UserId).NotEmpty();
        }
    }
}
