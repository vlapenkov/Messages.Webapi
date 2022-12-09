using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.ServiceProductNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    public interface IServiceProductsService
    {
        /// <summary>Создать услугу</summary>         
        [Post("/api/v1/ServiceProducts")]
        Task<long> CreateServiceProduct([Body] CreateServiceProductRequest request);

        /// <summary>Получить инфо о услуге</summary>  
        [Get("/api/v1/ServiceProducts/{id}")]
        Task<ServiceProductResponse> GetServiceProduct(long id);

        /// <summary>Апдейт услуги</summary>  
        [Put("/api/v1/ServiceProducts/{id}")]
        Task UpdateServiceProduct(long id, [Body] UpdateServiceProductRequest request);
    }
}