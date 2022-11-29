﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.ProductsNS.Commands.CreateProduct;
using Rk.Messages.Logic.ProductsNS.Commands.DeleteProduct;
using Rk.Messages.Logic.ProductsNS.Commands.UpdateProductAttributes;
using Rk.Messages.Logic.ProductsNS.Dto;
using Rk.Messages.Logic.ProductsNS.Queries.GetProductQuery;
using Rk.Messages.Logic.ProductsNS.Queries.GetProductsQuery;

namespace Rk.Messages.Webapi.Controllers
{
    /// <summary>
    /// Работа с продуктами
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController :ControllerBase
    {
        private readonly IMediator _mediator;

        /// <inheritdoc />
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создать продукцию
        /// </summary>
        /// <param name="request">запрос создания</param>
        /// <returns></returns>
        [HttpPost]
       // [Authorize(Roles = "manager,admin")]
        public async Task<long> CreateProduct([FromBody] CreateProductRequest request)
        {
            return await _mediator.Send(new CreateProductCommand { Request = request });
        }

        /// <summary>Получить список продукции с отбором и пагинацией </summary>
        [HttpGet]
        public async Task<PagedResponse<ProductShortDto>> GetProducts([FromQuery] FilterProductsRequest request)
        {
            var result = await _mediator.Send(new GetProductsQuery {Request = request });
            return result;
        }

        /// <summary>Получить информацию о продукции</summary>
        [HttpGet("{id:long}")]
        public async Task<ProductResponse> GetProduct(long id)
        {
            var result = await _mediator.Send(new GetProductQuery { Id=id });
            return result;
        }

        /// <summary>Апдейт значений атрибутов товара</summary>
        [HttpPut("{id:long}/attributes")]
        public async Task UpdateAttributes(long id, [FromBody] IReadOnlyCollection<AttributeValueDto> attributeValues)
        {
            await _mediator.Send(new UpdateProductAttributesCommand { ProductId = id , AttributeValues = attributeValues });            
        }

        /// <summary>Получить информацию об атрибутах всей продукции</summary>
        [HttpGet("attributes")]
        public async Task<IReadOnlyCollection<AttributeDto>> GetProductAttributes()
        {
            var result = await _mediator.Send(new GetProductAttributesQuery {  });
            return result;
        }

        [HttpDelete("{id:long}")]
        public async Task DeleteProductById(long id)
        {
            await _mediator.Send(new DeleteProductCommand { ProductId= id });            
        }

    }
}
