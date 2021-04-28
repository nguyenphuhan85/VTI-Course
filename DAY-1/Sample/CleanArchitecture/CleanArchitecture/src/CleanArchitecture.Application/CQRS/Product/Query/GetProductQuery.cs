using CleanArchitecture.Application.Models.Product.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.CQRS.Product.Query
{
    public class GetProductQuery : IRequest<Models.Product.Response.ProductResponseModel>
    {
        public Guid ProductID { get; set; }
    }
}
