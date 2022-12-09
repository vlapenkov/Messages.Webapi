using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Logic.WorkProductsNS.Dto;
using System.Threading.Tasks;
using Rk.Messages.Logic.WorkProductsNS.Commands;
using Rk.Messages.Logic.WorkProductsNS.Commands.CreateWorkProduct;
using Rk.Messages.Logic.WorkProductsNS.Commands.UpdateWorkProduct;
using Microsoft.AspNetCore.Authorization;
using Rk.Messages.Logic.ProductsNS.Queries.GetProductQuery;
using Rk.Messages.Logic.WorkProductsNS.Queries.GetWorkProduct;

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

        /// <summary>Получить информацию о продукции</summary>
        [AllowAnonymous]
        [HttpGet("{id:long}")]
        public async Task<WorkProductResponse> GetWorkProduct(long id)
        {
            var result = await _mediator.Send(new GetWorkProductQuery { Id = id });
            return result;
        }

        /// <summary>Апдейт работы</summary>
        [HttpPut("{id:long}")]
        public async Task UpdateWorkProduct(long id, [FromBody] UpdateWorkProductRequest request)
        {
             await _mediator.Send(new UpdateWorkProductCommand {ProductId = id,  Request = request });
        }
    }
}
