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
    public class FetchProductQueryHandler :  IRequestHandler<FetchProductQuery, IEnumerable<Models.Product.Response.ProductResponseModel>>
    {
        public readonly IProductService _productDataService;

        public FetchProductQueryHandler(IProductService productDataService)
        {
            _productDataService = productDataService;
        }
        public async Task<IEnumerable<Models.Product.Response.ProductResponseModel>> Handle(FetchProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _productDataService.FetchProduct();

            return (IEnumerable<Models.Product.Response.ProductResponseModel>)result;
        }

        
    }
}
