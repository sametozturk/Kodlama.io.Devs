using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Command.Register
{
    public class AuthorizationRegisterCommandValidator : AbstractValidator<AuthorizationRegisterCommand>
    {
        public AuthorizationRegisterCommandValidator()
        {
            RuleFor(r => r.UserForRegisterDto.Password).NotEmpty();
            RuleFor(r => r.UserForRegisterDto.FirstName).NotEmpty();
            RuleFor(r => r.UserForRegisterDto.LastName).NotEmpty();
            RuleFor(r => r.UserForRegisterDto.Password).MinimumLength(5);
            RuleFor(r => r.UserForRegisterDto.FirstName).MinimumLength(2);
            RuleFor(r => r.UserForRegisterDto.LastName).MinimumLength(2);
            RuleFor(r => r.UserForRegisterDto.Email).NotNull().EmailAddress();
        }
    }
}
