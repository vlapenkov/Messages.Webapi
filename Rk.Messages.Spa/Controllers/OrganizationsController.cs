

using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.OrganizationsNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly  IOrganizationsService _service;

        public OrganizationsController(IOrganizationsService service)
        {
            _service = service;
        }


        [HttpGet("{id:long}")]
        public async Task<OrganizationDto> GetOrganization(long id)
        {
            return await _service.GetOrganization(id);
        }
    }
}
