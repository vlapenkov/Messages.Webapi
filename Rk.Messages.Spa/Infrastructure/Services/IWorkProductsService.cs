using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;
using Rk.Messages.Spa.Infrastructure.Dto.WorkProductNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    public interface IWorkProductsService
    {
        /// <summary>Создать работу</summary>         
        [Post("/api/v1/WorkProducts")]
        Task<long> CreateWorkProduct([Body] CreateWorkProductRequest request);

        /// <summary>Получить инфо о работе</summary>  
        [Get("/api/v1/WorkProducts/{id}")]
        Task<WorkProductResponse> GetWorkProduct(long id);

        /// <summary>Апдейт работы</summary>  
        [Put("/api/v1/WorkProducts/{id}")]
        Task UpdateWorkProduct(long id, [Body] UpdateWorkProductRequest request);
    }
}
