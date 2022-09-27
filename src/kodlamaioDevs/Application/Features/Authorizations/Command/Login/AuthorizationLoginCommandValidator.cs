using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Command.Login
{
    public class AuthorizationLoginCommandValidator : AbstractValidator<AuthorizationLoginCommand>
    {
        public AuthorizationLoginCommandValidator()
        {
            RuleFor(l => l.UserForLoginDto.Email).NotEmpty();
            RuleFor(l => l.UserForLoginDto.Password).NotEmpty();
        }
    }
}
