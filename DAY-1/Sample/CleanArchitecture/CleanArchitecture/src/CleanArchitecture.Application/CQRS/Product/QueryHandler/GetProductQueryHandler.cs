using CleanArchitecture.Application.CQRS.Product.Query;
using CleanArchitecture.Application.DatabaseServices;
using CleanArchitecture.Application.Models.Product.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.CQRS.Product.QueryHandler
{
    public class GetProductQueryHandler :  IRequestHandler<GetProductQuery, ProductResponseModel>
    {
        public readonly IProductService _productDataService;

        public GetProductQueryHandler(IProductService productDataService)
        {
            _productDataService = productDataService;
        }
        public async Task<ProductResponseModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _productDataService.GetProduct(request.ProductID);

            return result;
        }
    }
}
