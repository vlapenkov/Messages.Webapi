﻿using Messages.Spa.Infrastructure.Dto.CommonNS;
using Messages.Spa.Infrastructure.Dto.ProductsNS;
using Messages.Spa.Infrastructure.Dto.SectionsNS;
using Messages.Spa.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messages.Spa.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductsService _productsService;


        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpPost]
        public async Task<long> CreateProduct([FromBody] CreateProductRequest request)
        {

            return await _productsService.CreateProduct(request);
        }

        /// <summary>Получить список товаров с отбором и пагинацией </summary>
        [HttpGet("list")]
        public async Task<PagedResponse<ProductShortDto>> GetProducts([FromQuery] FilterProductsRequest request)
        {

            return await _productsService.GetProducts(request);
        }
    }
}
