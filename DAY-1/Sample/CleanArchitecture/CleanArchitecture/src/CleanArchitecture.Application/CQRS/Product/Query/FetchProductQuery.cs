using CleanArchitecture.Application.Models.Product.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.CQRS.Product.Query
{
    public class FetchProductQuery : IRequest<IEnumerable<Models.Product.Response.ProductResponseModel>>
    {

    }
}
