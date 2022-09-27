using Application.Features.Technologies.Command.CreateTechnology;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Command.DeleteTechnology
{
    public class DeleteTechnologyCommandValidator: AbstractValidator<DeleteTechnologyCommand>
    {
        public DeleteTechnologyCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}
