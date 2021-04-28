using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Application.CQRS.Product.Command;
using CleanArchitecture.Application.CQRS.Product.Query;
using CleanArchitecture.Application.CQRS.ProductType.Query;
using CleanArchitecture.Application.DatabaseServices;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Application.Models.Notification;
using CleanArchitecture.Application.Models.Product.Response;
using CleanArchitecture.Application.Models.ProductType.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CleanArchitecture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private IMediator _mediator;
        public ProductController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }
        // We can update search criteria later
        [HttpGet]
        public async Task<IEnumerable<ProductResponseModel>> Get()
        {
            var query = new FetchProductQuery();
            return await _mediator.Send(query);
        }

        // GET api/values/{id}
        [HttpGet("{id}")]
        public async Task<ProductResponseModel> Get(Guid id)
        {
            var query = new GetProductQuery() { ProductID = id };
            return await _mediator.Send(query);
        }


        // POST
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] CreateProductCommand command)
        {

            return await _mediator.Send(command);
        }

        // PUT 
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdateProductCommand command)
        {
            command.ProductTypeID = id;
            return await _mediator.Send(command);
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            DeleteProductCommand deleteProductCommand = new DeleteProductCommand()
            {
                ProductID = id
            };
            return await _mediator.Send(deleteProductCommand);
        }
    }
}