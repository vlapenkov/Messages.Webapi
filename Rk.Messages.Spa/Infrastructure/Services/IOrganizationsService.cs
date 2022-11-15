﻿using Refit;
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

        /// <summary>Создать организацию</summary>         
        [Post("/api/v1/Organizations")]
        Task<long> CreateOrganization([Body] CreateOrganizationRequest request);
    }
}
