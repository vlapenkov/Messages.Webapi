using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Logic.OrdersNS.Queries.GetOrder;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganization;
using System.Threading.Tasks;

namespace Rk.Messages.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
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
    }
}
