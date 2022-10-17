using MediatR;
using Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using Messages.Logic.SectionsNS.Dto;
using Messages.Logic.SectionsNS.Queries.GetAllSections;
using Messages.Logic.SectionsNS.Queries.GetSectionsTree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>Создать раздел</summary>        
        [HttpPost]
        public async Task<long> CreateSection([FromBody] CreateSectionRequest request) {

           return await _mediatr.Send(new CreateSectionCommand { ParentSectionId = request.ParentSectionId, Name = request.Name });
        }

        /// <summary>Получить список разделов </summary>
        [HttpGet("list")]
        public async Task<IReadOnlyCollection<SectionDto>> GetSections([FromQuery] long? parentSectionId)
        {

            return await _mediatr.Send(new GetAllSectionsQuery {ParentSectionId = parentSectionId });
        }

        /// <summary>Получить дерево разделов </summary>
        [HttpGet("tree")]
        public async Task<SectionTreeNode> GetSectionsAsTree([FromQuery] long? parentSectionId)
        {

            var rootSection = await _mediatr.Send(new GetSectionTreeQuery { ParentSectionId = parentSectionId });

            return rootSection;          
        }

    }
}
