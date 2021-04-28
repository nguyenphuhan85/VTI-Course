using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.CQRS.ProductType.Command
{
    public class DeleteProductTypeCommand : IRequest<bool>
    {
        public Guid ProductTypeID { get; set; }
    }
}
