using Messages.Spa.Infrastructure.Dto.SectionsNS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messages.Spa.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private ISectionsServices _sectionsServices;
       

        public SectionsController(ISectionsServices sectionsServices)
        {
            _sectionsServices = sectionsServices;
        }

        [HttpPost]
        public async Task<long> CreateSection([FromBody] CreateSectionRequest request)
        {

            return await _sectionsServices.CreateSection(request);
        }

        [HttpGet("list")]
        public async Task<IReadOnlyCollection<SectionDto>> GetSectionsAsList([FromQuery] long? parentSectionId)
        {

            return await _sectionsServices.GetSectionsAsList(parentSectionId);
        }

        [HttpGet("tree")]
        public async Task<SectionTreeNode> GetSectionsAsTree([FromQuery] long? parentSectionId)
        {

            var result = await _sectionsServices.GetSectionsAsTree(parentSectionId);

            return result;
        }
    }
}
