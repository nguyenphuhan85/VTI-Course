using CleanArchitecture.Application.CQRS.ProductType.Query;
using CleanArchitecture.Application.DatabaseServices;
using CleanArchitecture.Application.Models.ProductType.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.CQRS.ProductType.QueryHandler
{
    public class GetProductTypeDetailsQueryHandler : IRequestHandler<GetProductTypeDetailsQuery, ProductTypeDetailsResponseModel>
    {
        private readonly IProductTypeService _productTypeDataService;

        public GetProductTypeDetailsQueryHandler(IProductTypeService productTypeDataService)
        {
            _productTypeDataService = productTypeDataService;
        }
        public async Task<ProductTypeDetailsResponseModel> Handle(GetProductTypeDetailsQuery request, CancellationToken cancellationToken)
        {
            var result = await _productTypeDataService.GetProductTypeDetails(request.ProductTypeId);

            return result;
        }
    }
}
