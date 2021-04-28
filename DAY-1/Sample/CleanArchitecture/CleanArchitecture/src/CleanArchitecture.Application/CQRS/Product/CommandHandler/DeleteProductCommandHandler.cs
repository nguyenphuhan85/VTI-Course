using CleanArchitecture.Application.CQRS.Product.Command;
using CleanArchitecture.Application.DatabaseServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.CQRS.Product.CommandHandler
{
    public class DeleteProductCommandHandler :  IRequestHandler<DeleteProductCommand, bool>
    {
        public readonly IProductService _productDataService;

        private readonly IMediator _mediator;

        public DeleteProductCommandHandler(IProductService productDataService, IMediator mediator)
        {
            _productDataService = productDataService;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productDataService.DeleteProduct(request);
            return result;
        }
    }
}
