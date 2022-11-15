using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Logic.OrdersNS.Queries.GetOrder;
using Rk.Messages.Logic.OrganizationsNS.Commands.CreateOrganization;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganization;
using Rk.Messages.Logic.ProductsNS.Commands.CreateProduct;
using System.Threading.Tasks;

namespace Rk.Messages.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:long}")]
        public async Task<OrganizationDto> GetOrganization(long id)
        {
            return await _mediator.Send(new GetOrganizationQuery { Id = id });
        }

        public async Task<long> CreateOrganization([FromBody] CreateOrganizationRequest request)
        {
            return await _mediator.Send(new CreateOrganizationCommand { Request = request });
        }
    }
}
