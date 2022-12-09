using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    public interface IWorkProductsService
    {
        /// <summary>Создать продукцию</summary>         
        [Post("/api/v1/WorkProducts")]
        Task<long> CreateWorkProduct([Body] CreateWorkProductRequest request);
    }
}
