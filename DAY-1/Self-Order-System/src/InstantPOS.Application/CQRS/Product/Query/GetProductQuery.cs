using System;
using System.Collections.Generic;
using InstantPOS.Application.Models.Product;
using MediatR;

namespace InstantPOS.Application.CQRS.Product.Query
{
    public class GetProductQuery : IRequest<ProductResponseModel>
    {
public Guid ProductID { get; set; }
    }
}
