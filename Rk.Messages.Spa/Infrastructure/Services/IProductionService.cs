﻿using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    /// <summary>
    /// Сервис для работы с продукцией
    /// </summary>
    public interface IProductionsService
    {   

        /// <summary>Получить пагинированный список продукции</summary>  
        [Get("/api/v1/Productions")]
        Task<PagedResponse<ProductShortDto>> GetProducts([Query] FilterProductsRequest request);                

        /// <summary>Получить инфо о продукции</summary>  
        [Get("/api/v1/Productions/attributes")]
        Task<IReadOnlyCollection<AttributeDto>> GetProductAttributes();

        /// <summary>Физически удалить продукцию</summary>  
        [Delete("/api/v1/Productions/{id}")]
        Task DeleteProductById(long id);               

        /// <summary>Установить статус продукции</summary>  
        [Patch("/api/v1/Products/{id}/status")]
        Task SetStatus(long id, [Body] long status);

        /// <summary>Добавить отзыв о продукции</summary>  
        [Post("/api/v1/Productions/{id}/reviews")]
        Task AddReview(long id, [Body] CreateReviewRequest request);
    }
}
