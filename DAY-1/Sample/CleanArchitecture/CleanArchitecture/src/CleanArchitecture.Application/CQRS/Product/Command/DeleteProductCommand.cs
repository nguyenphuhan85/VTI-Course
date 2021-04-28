using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.CQRS.Product.Command
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid ProductID { get; set; }

    }
}
