using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Logic.RegionsNS.Queries.GetRegions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rk.Messages.Webapi.Controllers
{
    /// <summary>Управление регионами</summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IMediator _mediator;
               
        public RegionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Получить список регионов</summary>
        [HttpGet]
        public async Task<IReadOnlyCollection<string>> GetRegions()
        {
            return await _mediator.Send(new GetRegionsQuery());
        }
    }
}
