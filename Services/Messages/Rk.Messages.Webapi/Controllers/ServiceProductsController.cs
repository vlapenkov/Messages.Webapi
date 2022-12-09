using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Rk.Messages.Logic.ServiceProductsNS.Dto;
using Rk.Messages.Logic.ServiceProductsNS.Commands.CreateServiceProduct;
using Rk.Messages.Logic.ServiceProductsNS.Queries.GetServiceProduct;
using Rk.Messages.Logic.ServiceProductsNS.Commands.UpdateServiceProduct;

namespace Rk.Messages.Webapi.Controllers
{
    /// <summary>
    /// Управление услугами
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServiceProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <inheritdoc />
        public ServiceProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Создать услугу</summary>
        [HttpPost]
        public async Task<long> CreateServiceProduct([FromBody] CreateServiceProductRequest request)
        {

           return await _mediator.Send(new CreateServiceProductCommand {Request = request });

        }

        /// <summary>Получить информацию о услуге</summary>
        [AllowAnonymous]
        [HttpGet("{id:long}")]
        public async Task<ServiceProductResponse> GetServiceProduct(long id)
        {
            var result = await _mediator.Send(new GetServiceProductQuery { Id = id });
            return result;
        }

        /// <summary>Апдейт услуги</summary>
        [HttpPut("{id:long}")]
        public async Task UpdateServiceProduct(long id, [FromBody] UpdateServiceProductRequest request)
        {
             await _mediator.Send(new UpdateServiceProductCommand {ProductId = id,  Request = request });
        }
    }
}
