using Microsoft.AspNetCore.Mvc;

namespace FileStore.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]    
    public class FilesController : ControllerBase
    {

        [HttpPost]
        public async Task<long> CreateFile()
        {

            
            return 10L;
        }

        /// <summary>Получить список разделов </summary>
        [HttpGet]
        public async Task<IReadOnlyCollection<int>> GetFiles()
        {

            return new[] { 1, 2, 3 };
        }
    }
}
