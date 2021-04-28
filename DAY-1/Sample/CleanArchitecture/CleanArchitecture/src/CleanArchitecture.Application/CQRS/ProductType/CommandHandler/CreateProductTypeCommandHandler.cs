﻿using CleanArchitecture.Application.CQRS.ProductType.Command;
using CleanArchitecture.Application.DatabaseServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.CQRS.ProductType.CommandHandler
{
    public class CreateProductTypeCommandHandler : IRequestHandler<CreateProductTypeCommand, bool>
    {

        public readonly IProductTypeService _productTypeDataService;
        private readonly IMediator _mediator;
        public CreateProductTypeCommandHandler(IProductTypeService productTypeDataService, IMediator mediator)
        {
            _productTypeDataService = productTypeDataService;
            _mediator = mediator;
        }

        public async Task<bool> Handle(CreateProductTypeCommand createProductTypeCommandRequest, CancellationToken cancellationToken)
        {
            var result = await _productTypeDataService.CreateProductType(createProductTypeCommandRequest);
            return result;
        }
    }
}
