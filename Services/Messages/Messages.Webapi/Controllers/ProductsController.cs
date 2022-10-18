using MediatR;
using Messages.Logic.ProductsNS.Commands.CreateProduct;
using Messages.Logic.ProductsNS.Dto;
using Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Messages.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController :ControllerBase
    {
        private readonly IMediator _mediatr;

        public ProductsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<long> CreateProduct([FromBody] CreateProductRequest request)
        {

            return await _mediatr.Send(new CreateProductCommand { Request = request });
        }
    }
}
