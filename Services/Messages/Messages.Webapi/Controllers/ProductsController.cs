using MediatR;
using Messages.Logic.CommonNS.Dto;
using Messages.Logic.ProductsNS.Commands.CreateProduct;
using Messages.Logic.ProductsNS.Dto;
using Messages.Logic.ProductsNS.Queries;
using Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using Messages.Logic.SectionsNS.Queries.GetAllSections;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

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

        /// <summary>Получить список товаров с отбором и пагинацией </summary>
        [HttpGet("list")]
        public async Task<PagedResponse<ProductShortDto>> GetProducts([FromQuery] FilterProductsRequest request)
        {

            var result = await _mediatr.Send(new GetProductsQuery {Request = request });

            return result;
        }
    }
}
