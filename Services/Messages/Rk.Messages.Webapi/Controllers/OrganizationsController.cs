using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.OrganizationsNS.Commands.CreateOrganization;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganization;
using Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganizations;
using System.Threading.Tasks;

namespace Rk.Messages.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
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

        [HttpGet()]
        public async Task<PagedResponse<OrganizationDto>> GetOrganizations()
        {
            return await _mediator.Send(new GetOrganizationsQuery { });
        }

        [HttpPost]
        public async Task<long> CreateOrganization([FromBody] CreateOrganizationRequest request)
        {
            return await _mediator.Send(new CreateOrganizationCommand { Request = request });
        }
    }
}
