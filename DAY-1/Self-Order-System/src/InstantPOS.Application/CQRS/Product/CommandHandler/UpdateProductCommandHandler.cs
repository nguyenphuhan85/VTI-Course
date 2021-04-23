using System.Threading;
using System.Threading.Tasks;
using InstantPOS.Application.CQRS.Product.Command;
using InstantPOS.Application.DatabaseServices.Interfaces;
using MediatR;

namespace InstantPOS.Application.CQRS.Product.CommandHandler
{
    public class UpdateProductCommandHandler : BaseProductHandler, IRequestHandler<UpdateProductCommand, bool>
    {
        public UpdateProductCommandHandler(IProductDataService productDataService) : base(productDataService)
        {
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await base._productDataService.UpdateProduct(request);
            return result;
        }
    }
}
