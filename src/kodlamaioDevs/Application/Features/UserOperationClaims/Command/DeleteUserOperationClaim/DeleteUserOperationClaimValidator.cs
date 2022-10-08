using Application.Features.Languages.Commands.DeleteLanguage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Command.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimValidator : AbstractValidator<DeleteUserOperationClaimCommand>
    {
        public DeleteUserOperationClaimValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}
