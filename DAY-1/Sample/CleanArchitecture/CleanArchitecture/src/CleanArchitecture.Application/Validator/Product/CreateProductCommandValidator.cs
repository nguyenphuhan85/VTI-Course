using CleanArchitecture.Application.CQRS.Product.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Validator.Product
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {

            RuleFor(v => v.ProductName)
                .MaximumLength(250)
                .NotEmpty();

            RuleFor(v => v.ProductKey)
               .MaximumLength(50)
               .NotEmpty();
        }
    }
}
