using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS;
using Rk.Messages.Spa.Infrastructure.Dto.NewsNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    /// <summary>Управление новостями</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        private readonly IFileStoreService _filesService;
                

        public NewsController(INewsService newsService, IFileStoreService filesService)
        {
            _newsService = newsService;
            _filesService = filesService;
        }

        /// <summary>Создать новость</summary>
        [HttpPost]
        public async Task<long> CreateNews([FromBody] CreateNewsRequest request)
        {
            var document = request.Document;

            if (document != null)

            {
                var fileRequest = new CreateFileRequest { FileName = document.FileName, Data = document.Data };

                var fileGlobalIds = await _filesService.CreateFiles(new[] { fileRequest });

                var fileGlobalIdsArray = fileGlobalIds.ToArray();
            }

            return await _newsService.CreateNews(request);
        }

        /// <summary>Получить информацию о новости</summary>
        [HttpGet("{id:long}")]
        public async Task<NewsResponse> GetNews(long id)
        {
            return await _newsService.GetNews(id);
        }

        /// <summary>Получить список новостей</summary>
        [HttpGet]
        public async Task<PagedResponse<NewsResponse>> GetNews()
        {
            return await _newsService.GetNews();
        }

        /// <summary>Удалить новость</summary>
        [HttpDelete("{id:long}")]
        public async Task DeleteNewsById(long id)
        {
            await _newsService.DeleteById(id);
        }
    }
}
