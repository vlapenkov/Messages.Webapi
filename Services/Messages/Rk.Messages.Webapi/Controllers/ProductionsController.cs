﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Domain.Enums;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.ProductsNS.Commands.AddExchange;
using Rk.Messages.Logic.ProductsNS.Commands.AddReview;
using Rk.Messages.Logic.ProductsNS.Commands.DeleteProduct;
using Rk.Messages.Logic.ProductsNS.Commands.SetStatus;
using Rk.Messages.Logic.ProductsNS.Dto;
using Rk.Messages.Logic.ProductsNS.Queries.GetProductsExchanges;
using Rk.Messages.Logic.ProductsNS.Queries.GetProductsQuery;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rk.Messages.Webapi.Controllers
{
    /// <summary> Управление продукцией, услугами, технологиями </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <inheritdoc />
        public ProductionsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>Получить список продукции с отбором и пагинацией </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<PagedResponse<ProductShortDto>> GetProductions([FromQuery] FilterProductsRequest request)
        {
            var result = await _mediator.Send(new GetProductionsQuery { Request = request });
            return result;
        }

        /// <summary>Физически удалить продукцию</summary>
        [HttpDelete("{id:long}")]
        public async Task DeleteProductById(long id)
        {
            await _mediator.Send(new DeleteProductCommand { ProductId = id });
        }

        /// <summary>Установить статус</summary>
        [HttpPatch("{id:long}/status")]
        public async Task SetStatus(long id, [FromBody] ProductStatus status)
        {
            await _mediator.Send(new SetStatusCommand { ProductId = id, Status = status });
        }

        /// <summary>Получить информацию об атрибутах всей продукции</summary>
        [HttpGet("attributes")]
        [AllowAnonymous]
        public async Task<IReadOnlyCollection<AttributeDto>> GetProductAttributes()
        {
            var result = await _mediator.Send(new GetProductAttributesQuery { });
            return result;
        }

        /// <summary>Добавить отзыв о продукции</summary>  
        [HttpPost("{id:long}/reviews")]
        public async Task AddReview(long id, [FromBody] CreateReviewRequest request)
        {
            await _mediator.Send(new AddReviewCommand { ProductId = id, Request = request });
        }

        /// <summary>Добавить информацию о регистрации</summary>  
        [HttpPost("exchanges")]
        public async Task RegisterExchange( [FromBody] RegisterProductsExchangeRequest request)
        {
            await _mediator.Send(new RegisterProductExchangeCommand { ExchangeType = request.ExchangeType,ProductsLoaded = request.ProductsLoaded });
        }

        /// <summary>Получить информацию о регистрации</summary>  
        [HttpGet("exchanges")]
        public async Task<IReadOnlyCollection<ProductsExchangeDto>> GetExchanges()
        {
            return await _mediator.Send(new GetProductsExchangesQuery() );
        }

    }
}
