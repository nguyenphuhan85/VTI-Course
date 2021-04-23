using System.Threading;
using System.Threading.Tasks;
using InstantPOS.Application.CQRS.Product.Command;
using InstantPOS.Application.DatabaseServices.Interfaces;
using MediatR;

namespace InstantPOS.Application.CQRS.Product.CommandHandler
{
    public class DeleteProductCommandHandler : BaseProductHandler, IRequestHandler<DeleteProductCommand, bool>
    {
        public DeleteProductCommandHandler(IProductDataService productDataService) : base(productDataService)
        {
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await base._productDataService.DeleteProduct(request.ProductID);
            return result;
        }
    }
}
