using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InstantPOS.Application.CQRS.Product.Query;
using InstantPOS.Application.DatabaseServices.Interfaces;
using InstantPOS.Application.Models.Product;
using MediatR;

namespace InstantPOS.Application.CQRS.Product.QueryHandler
{
    public class GetProductQueryHandler : BaseProductHandler, IRequestHandler<GetProductQuery, ProductResponseModel>
    {
        public GetProductQueryHandler(IProductDataService productDataService): base(productDataService)
        {
        }
        public async Task<ProductResponseModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _productDataService.GetProduct(request.ProductID);

            return result;
        }
    }
}
