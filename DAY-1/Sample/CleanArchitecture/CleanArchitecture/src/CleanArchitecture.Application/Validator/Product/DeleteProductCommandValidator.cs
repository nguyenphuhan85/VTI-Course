using CleanArchitecture.Application.CQRS.Product.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Validator.Product
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.ProductID).Must(guid => GuidValidator.IsGuid(guid.ToString())).WithMessage("GUUID invalid");
        }
    }
}
