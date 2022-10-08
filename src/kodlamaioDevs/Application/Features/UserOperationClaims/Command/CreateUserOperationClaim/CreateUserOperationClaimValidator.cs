using Application.Features.Languages.Commands.CreateBrand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Command.CreateUserOperationClaim
{
    public class CreateUserOperationClaimValidator: AbstractValidator<CreateUserOperationClaimCommand>
    {
        public CreateUserOperationClaimValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.OperationClaimId).NotNull();
            RuleFor(x => x.OperationClaimId).NotEmpty();
        }
    }
}
