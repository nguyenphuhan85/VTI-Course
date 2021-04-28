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
    public class FetchProductTypeQueryHandler : IRequestHandler<FetchProductTypeQuery, IEnumerable<ProductTypeResponseModel>>
    {
        private readonly IProductTypeService _productTypeDataService;

        public FetchProductTypeQueryHandler(IProductTypeService productTypeDataService)
        {
            _productTypeDataService = productTypeDataService;
        }

        public async Task<IEnumerable<ProductTypeResponseModel>> Handle(FetchProductTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _productTypeDataService.FetchProductType();

            return result;
        }
    }
}
