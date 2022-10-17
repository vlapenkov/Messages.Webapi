using Messages.Spa.Infrastructure.Dto.ProductsNS;
using Refit;
using System.Threading.Tasks;

namespace Messages.Spa.Infrastructure.Services
{
    public interface IProductsService
    {
        /// <summary>Создать раздел</summary>         
        [Post("/api/v1/Products")]
        Task<long> CreateProduct([Body] CreateProductRequest request);
    }
}
