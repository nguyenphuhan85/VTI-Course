using System;
using System.Collections.Generic;
using InstantPOS.Application.Models.Product;
using MediatR;

namespace InstantPOS.Application.CQRS.Product.Query
{
    public class FetchProductQuery : IRequest<IEnumerable<ProductResponseModel>>
    {

    }
}
