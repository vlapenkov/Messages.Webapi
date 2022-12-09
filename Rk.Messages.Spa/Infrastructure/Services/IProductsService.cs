using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    /// <summary>
    /// Сервис для работы с продукцией
    /// </summary>
    public interface IProductsService
    {
        /// <summary>Создать продукцию</summary>         
        [Post("/api/v1/Products")]
        Task<long> CreateProduct([Body] CreateProductRequest request);       

        /// <summary>Получить инфо о продукции</summary>  
        [Get("/api/v1/Products/{id}")]
        Task<ProductResponse> GetProduct(long id);          

        /// <summary>Апдейт товара</summary>  
        [Put("/api/v1/Products/{id}")]
        Task UpdateProduct(long id, [Body]UpdateProductRequest request);
        
    }
}
