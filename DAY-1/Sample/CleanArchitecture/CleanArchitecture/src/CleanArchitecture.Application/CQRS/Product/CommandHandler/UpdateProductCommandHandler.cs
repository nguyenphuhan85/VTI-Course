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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        public readonly IProductService _productDataService;

        private readonly IMediator _mediator;

        public UpdateProductCommandHandler(IProductService productDataService, IMediator mediator)
        {
            _productDataService = productDataService;
            _mediator = mediator;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productDataService.UpdateProduct(request);
            return result;
        }
    }
}
