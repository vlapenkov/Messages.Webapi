using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS;
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
        private readonly ISectionsServices _sectionsServices;

        private readonly IFileStoreService _filesService;


        public SectionsController(ISectionsServices sectionsServices, IFileStoreService filesService)
        {
            _sectionsServices = sectionsServices;
            _filesService = filesService;
        }

        /// <summary>
        /// Создать раздел
        /// </summary>        
        [HttpPost]
        public async Task<long> CreateSection([FromBody] CreateSectionRequest request)
        {
            return await _sectionsServices.CreateSection(request);
        }

        /// <summary>Создать/обновить документ для раздела</summary>        
        [HttpPut("{sectionId:long}/document")]
        public async Task UpsertDocument(long sectionId, [FromBody] FileDataDto document)
        {
            var request = new CreateFileRequest { FileName = document.FileName, Data = document.Data };

            var fileGlobalIds = await _filesService.CreateFiles(new[] { request });

            document.FileId = fileGlobalIds.First();

            await _sectionsServices.UpsertDocument(sectionId, document);
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


        /// <summary>Удалить раздел </summary>
        [HttpDelete("{id:long}")]
        public async Task DeleteSectionById(long id)
        {
            await _sectionsServices.DeleteSectionById(id);
        }
    }
}
