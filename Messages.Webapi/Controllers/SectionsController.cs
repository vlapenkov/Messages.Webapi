using MediatR;
using Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using Messages.Logic.SectionsNS.Dto;
using Messages.Logic.SectionsNS.Queries.GetAllSections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messages.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public SectionsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<long> CreateSection([FromBody] CreateSectionRequest request) {

           return await _mediatr.Send(new CreateSectionCommand { ParentSectionId = request.ParentSectionId, Name = request.Name });
        }

        //[HttpGet("{id:long}")]
        //public async Task<SectionDto> GetSection([FromRoute] long? parentSectionId)
        //{

        //    return await _mediatr.Send(new GetAllSectionsQuery { ParentSectionId = parentSectionId });
        //}

        [HttpGet]
        public async Task<IReadOnlyCollection<SectionDto>> GetSections([FromQuery] long? parentSectionId)
        {

            return await _mediatr.Send(new GetAllSectionsQuery {ParentSectionId = parentSectionId });
        }

    }
}
