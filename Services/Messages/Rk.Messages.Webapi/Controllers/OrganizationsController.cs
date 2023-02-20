using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Domain.Enums;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.OrganizationsNS.Commands.CreateOrganization;
using Rk.Messages.Logic.OrganizationsNS.Commands.SetStatus;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganization;
using Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganizations;
using System.Threading.Tasks;
using Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganizationByInn;

namespace Rk.Messages.Webapi.Controllers
{
    /// <summary>  Контроллер работы с организациями </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrganizationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <inheritdoc />
        public OrganizationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary> Получить организацию по id </summary>
        [HttpGet("{id:long}")]
        public async Task<OrganizationDto> GetOrganization(long id)
        {
            return await _mediator.Send(new GetOrganizationQuery { Id = id });
        }
        
        /// <summary> Получить организацию по ИНН </summary>
        [HttpGet("inn/{inn}")]
        public async Task<OrganizationDto> GetOrganization(string inn)
        {
            return await _mediator.Send(new GetOrganizationByInnQuery { Inn = inn });
        }
        
        /// <summary> Получить список организаций </summary>
        [HttpGet()]
        public async Task<PagedResponse<OrganizationDto>> GetOrganizations()
        {
            return await _mediator.Send(new GetOrganizationsQuery { });
        }

        /// <summary> Создать новую организацию </summary>
        //[HttpPost]
        //public async Task<long> CreateOrganization([FromBody] CreateOrganizationRequest request)
        //{
        //    return await _mediator.Send(new CreateOrganizationCommand { Request = request });
        //}

        /// <summary>Установить статус</summary>
        [HttpPatch("{id:long}/status")]
        public async Task SetStatus(long id, [FromBody] long status)
        {
            await _mediator.Send(new SetStatusCommand { OrganizationId = id, Status = (OrganizationStatus) status });
        }
    }
}
