using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.NewsNS.Commands.CreateNews;
using Rk.Messages.Logic.NewsNS.Commands.DeleteNews;
using Rk.Messages.Logic.NewsNS.Dto;
using Rk.Messages.Logic.NewsNS.Queries.GetNews;
using Rk.Messages.Logic.NewsNS.Queries.GetOneNews;
using System.Threading.Tasks;

namespace Rk.Messages.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Создать новость</summary>
        [HttpPost]
        public async Task<long> CreateNews([FromBody] CreateNewsRequest request)
        {

            return await _mediator.Send(new CreateNewsCommand { Request = request });
        }

        /// <summary>Получить информацию о новости</summary>
        [HttpGet("{id:long}")]
        public async Task<NewsResponse> GetNews(long id)
        {
            var result = await _mediator.Send(new GetOneNewsQuery { Id = id });

            return result;
        }

        [HttpGet]
        public async Task<PagedResponse<NewsResponse>> GetNews()
        {
            return await _mediator.Send(new GetAllNewsQuery { Request = new NewsRequest { } });
        }

        /// <summary>Удалить новость</summary>
        [HttpDelete("{id:long}")]
        public async Task DeleteNewsById(long id)
        {
             await _mediator.Send(new DeleteNewsCommand { Id = id});
        }
    }
}
