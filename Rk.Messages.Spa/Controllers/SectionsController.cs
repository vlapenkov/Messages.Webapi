using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.SectionsNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    /// <summary>
    /// Управление разделами в каталоге
    /// </summary>
    
    [Route( "api/[controller]" )]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private ISectionsServices _sectionsServices;
       

        public SectionsController(ISectionsServices sectionsServices)
        {
            _sectionsServices = sectionsServices;
        }

        /// <summary>
        /// Создать раздел
        /// </summary>        
        [HttpPost]
        public async Task<long> CreateSection([FromBody] CreateSectionRequest request)
        {
            return await _sectionsServices.CreateSection(request);
        }

        /// <summary>
        /// Получить список разделов
        /// </summary>
        [HttpGet("list")]
        public async Task<IReadOnlyCollection<SectionDto>> GetSectionsAsList([FromQuery] long? parentSectionId)
        {
            return await _sectionsServices.GetSectionsAsList(parentSectionId);
        }

        /// <summary>
        /// Получить дерево разделов
        /// </summary>
        [HttpGet("tree")]
        public async Task<SectionTreeNode> GetSectionsAsTree([FromQuery] long? parentSectionId)
        {

            var result = await _sectionsServices.GetSectionsAsTree(parentSectionId);

            return result;
        }
    }
}
