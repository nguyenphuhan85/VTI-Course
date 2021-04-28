using CleanArchitecture.Application.CQRS.ProductType.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Validator.ProductType
{
    public class CreateProductTypeCommandValidator : AbstractValidator<CreateProductTypeCommand>
    {
        public CreateProductTypeCommandValidator()
        {
          
            RuleFor(v => v.ProductTypeName)
                .MaximumLength(250)
                .NotEmpty();

            RuleFor(v => v.ProductTypeKey)
               .MaximumLength(50)
               .NotEmpty();
        }
    }
}
