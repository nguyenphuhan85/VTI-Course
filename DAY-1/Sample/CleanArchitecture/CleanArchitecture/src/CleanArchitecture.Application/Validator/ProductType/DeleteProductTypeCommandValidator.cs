using CleanArchitecture.Application.CQRS.ProductType.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Validator.ProductType
{
    public class DeleteProductTypeCommandValidator : AbstractValidator<DeleteProductTypeCommand>
    {
        public DeleteProductTypeCommandValidator()
        {
            RuleFor(x => x.ProductTypeID).Must(guid => GuidValidator.IsGuid(guid.ToString())).WithMessage("GUUID invalid");
        }
    }
}
