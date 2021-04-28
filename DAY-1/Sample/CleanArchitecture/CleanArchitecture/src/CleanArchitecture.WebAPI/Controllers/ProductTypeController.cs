using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Application.CQRS.ProductType.Command;
using CleanArchitecture.Application.CQRS.ProductType.Query;
using CleanArchitecture.Application.DatabaseServices;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Application.Models.ProductType.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : BaseApiController
    {
        private IMediator _mediator;
        public ProductTypeController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        // We can update search criteria later
        [HttpGet]
        public async Task<IEnumerable<ProductTypeResponseModel>> Get()
        {
            var query = new FetchProductTypeQuery();
            return await _mediator.Send(query);
        }

        // GET api/values/{id}
        [HttpGet("{id}")]
        public async Task<ProductTypeDetailsResponseModel> Get(Guid id)
        {
            var query = new GetProductTypeDetailsQuery() { ProductTypeId = id };
            return await _mediator.Send(query);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] CreateProductTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        // PUT 
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdateProductTypeCommand command)
        {
            command.ProductTypeID = id;
            return await _mediator.Send(command);
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            DeleteProductTypeCommand deleteProductCommand = new DeleteProductTypeCommand()
            {
                ProductTypeID = id
            };
            return await _mediator.Send(deleteProductCommand);
        }
    }
}