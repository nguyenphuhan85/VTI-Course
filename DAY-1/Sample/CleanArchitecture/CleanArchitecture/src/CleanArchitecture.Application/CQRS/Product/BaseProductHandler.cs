using CleanArchitecture.Application.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.CQRS.Product
{
    public class BaseProductHandler
    {
        public readonly IProductService _productDataService;
        public BaseProductHandler(IProductService productDataService)
        {
            _productDataService = productDataService;
        }
    }
}
