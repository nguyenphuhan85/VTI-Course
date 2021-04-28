using CleanArchitecture.Application.CQRS.Product.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Validator.Product
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.ProductID).Must(guid => GuidValidator.IsGuid(guid.ToString())).WithMessage("GUUID invalid");
            RuleFor(x => x.ProductTypeID).Must(guid => GuidValidator.IsGuid(guid.ToString())).WithMessage("GUUID invalid");

            RuleFor(v => v.ProductName)
                .MaximumLength(250)
                .NotEmpty();

            RuleFor(v => v.ProductKey)
               .MaximumLength(50)
               .NotEmpty();
        }
    }
}
