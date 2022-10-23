using Messages.Spa.Infrastructure.Dto.CommonNS;
using Messages.Spa.Infrastructure.Dto.ProductsNS;
using Refit;
using System.Threading.Tasks;

namespace Messages.Spa.Infrastructure.Services
{
    public interface IProductsService
    {
        /// <summary>Создать продукцию</summary>         
        [Post("/api/v1/Products")]
        Task<long> CreateProduct([Body] CreateProductRequest request);

        /// <summary>Получить пагинированный список продукции</summary>  
        [Get("/api/v1/Products/list")]
        Task<PagedResponse<ProductShortDto>> GetProducts([Body] FilterProductsRequest request);
    }
}
