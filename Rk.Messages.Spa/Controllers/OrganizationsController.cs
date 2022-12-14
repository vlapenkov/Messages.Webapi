

using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS;
using Rk.Messages.Spa.Infrastructure.Dto.OrganizationsNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly  IOrganizationsService _service;
        
        private readonly IFileStoreService _filesService;

        public OrganizationsController(IOrganizationsService service, IFileStoreService filesService)
        {
            _service = service;

            _filesService = filesService;
        }
        /// <summary>Получить организацию</summary>        
        [HttpGet("{id:long}")]
        public async Task<OrganizationDto> GetOrganization(long id)
        {
            return await _service.GetOrganization(id);
        }

        /// <summary>Получить организации</summary>
        [HttpGet]
        public async Task<PagedResponse<OrganizationDto>> GetOrganizations()
        {
            return await _service.GetOrganizations();
        }

        /// <summary>Создать организацию</summary>
        [HttpPost]
        public async Task<long> CreateOrganization([FromBody] CreateOrganizationRequest request)
        {
            if (request.Document != null)
            {
                CreateFileRequest fileRequest = new CreateFileRequest { FileName = request.Document.FileName, Data = request.Document.Data };

                var fileGlobalIds = await _filesService.CreateFiles(new[] { fileRequest });

                request.Document.FileId = fileGlobalIds.First(); 
            }

            return await _service.CreateOrganization(request);
        }
        
    }
}
