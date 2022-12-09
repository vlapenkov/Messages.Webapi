using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Logic.WorkProductsNS.Dto;
using System.Threading.Tasks;
using Rk.Messages.Logic.WorkProductsNS.Commands;
using Rk.Messages.Logic.WorkProductsNS.Commands.CreateWorkProduct;

namespace Rk.Messages.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorkProductsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public WorkProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>Создать работу</summary>
        [HttpPost]
        public async Task<long> CreateWorkProduct([FromBody] CreateWorkProductRequest request)
        {

           return await _mediator.Send(new CreateWorkProductCommand {Request = request });

        }
    }
}
