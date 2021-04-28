using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public BaseApiController()
        {
            if (_mediator == null)
            {
                _mediator = HttpContext.RequestServices.GetService<IMediator>();
            }
        }
    }
}
