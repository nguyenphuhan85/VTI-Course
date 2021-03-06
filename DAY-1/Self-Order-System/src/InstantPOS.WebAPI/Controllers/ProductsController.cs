using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstantPOS.Application.CQRS.Product.Command;
using InstantPOS.Application.CQRS.Product.Query;
using InstantPOS.Application.CQRS.ProductType.Query;
using InstantPOS.Application.Models.Product;
using InstantPOS.Application.Models.ProductType;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InstantPOS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : CustomBaseApiController
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {

        }
        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<ProductResponseModel>> Get()
        {
            var query = new FetchProductQuery();
            return await Mediator.Send(query);
        }

        // GET api/values/{id}
        [HttpGet("{id}")]
        public async Task<ProductResponseModel> Get(Guid id)
        {
            var query = new GetProductQuery() { ProductID=id};
            return await Mediator.Send(query);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdateProductCommand command)
        {
            command.ProductTypeID = id;
            return await Mediator.Send(command);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            DeleteProductCommand deleteProductCommand = new DeleteProductCommand() { ProductID=id};
            return await Mediator.Send(deleteProductCommand);
            }
        }
    }

