using CleanArchitecture.Application.CQRS.Product.Command;
using CleanArchitecture.Application.DatabaseServices;
using CleanArchitecture.Application.Models.Notification;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.CQRS.Product.CommandHandler
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        public readonly IProductService _productDataService;

        private readonly IMediator _mediator;
     
        public CreateProductCommandHandler(IProductService productDataService, IMediator mediator)
        {
            _productDataService = productDataService;
            _mediator = mediator;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productDataService.CreateProduct(request);

            var notification = new PushNotification
            {
                LastDate = DateTime.Now,
                Message = JsonConvert.SerializeObject(request)
            };
            await _mediator.Publish(notification,cancellationToken);

            return result;
        }
    }
}
