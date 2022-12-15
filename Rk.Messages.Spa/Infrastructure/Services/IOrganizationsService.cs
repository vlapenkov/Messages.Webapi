using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.OrganizationsNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    /// <summary>
    /// Сервис для работы с организациями
    /// </summary>
    public interface IOrganizationsService
    {
        /// <summary>Получить инфо об организации</summary>  
        [Get("/api/v1/Organizations/{id}")]
        Task<OrganizationDto> GetOrganization(long id);

        /// <summary>Получить инфо об организации</summary>  
        [Get("/api/v1/Organizations")]
        Task<PagedResponse<OrganizationDto>> GetOrganizations();

        /// <summary>Создать организацию</summary>         
        [Post("/api/v1/Organizations")]
        Task<long> CreateOrganization([Body] CreateOrganizationRequest request);

        /// <summary>Установить статус организации</summary>  
        [Patch("/api/v1/Organizations/{id}/status")]
        Task SetStatus(long id, [Body]  long status);
    }
}
