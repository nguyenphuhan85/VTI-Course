using CleanArchitecture.Application.Models.ProductType.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.CQRS.ProductType.Query
{
    public class GetProductTypeDetailsQuery : IRequest<ProductTypeDetailsResponseModel>
    {
        public Guid ProductTypeId { get; set; }
    }
}
