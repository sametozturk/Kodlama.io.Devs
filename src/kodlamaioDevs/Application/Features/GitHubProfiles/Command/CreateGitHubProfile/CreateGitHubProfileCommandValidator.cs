using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Command.CreateGitHubProfile
{
    public class CreateGitHubProfileCommandValidator : AbstractValidator<CreateGitHubProfileCommand>
    {
        public CreateGitHubProfileCommandValidator()
        {
            RuleFor(g => g.GitHubUrl).NotEmpty();
            RuleFor(g => g.UserId).NotEmpty();
        }
    }
}
