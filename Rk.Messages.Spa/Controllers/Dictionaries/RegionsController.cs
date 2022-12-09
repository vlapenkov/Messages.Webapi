using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    /// <summary>Управление регионами</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionsService _regionsService;

        public RegionsController(IRegionsService regionsService)
        {
            _regionsService = regionsService;
        }

        /// <summary>Получить список регионов</summary>
        [HttpGet]
        public async Task<IReadOnlyCollection<string>> GetRegions()
        {
            return await _regionsService.GetRegions();
        }
    }
}
