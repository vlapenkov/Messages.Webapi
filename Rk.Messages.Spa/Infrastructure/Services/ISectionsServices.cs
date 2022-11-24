using Microsoft.AspNetCore.Mvc;
using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.SectionsNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    public interface ISectionsServices
    {
        /// <summary>Создать раздел</summary>         
        [Post("/api/v1/Sections")]
        Task<long> CreateSection([Body] CreateSectionRequest request);

        /// <summary>Создать документ для раздела</summary>         
        [Put("/api/v1/Sections/{sectionId}/document")]
        Task UpsertDocument(long sectionId,[Body] FileDataDto document);

        /// <summary>Получить список разделов </summary>
        [Get("/api/v1/Sections/list")]
        Task<IReadOnlyCollection<SectionDto>> GetSectionsAsList([Query] long? parentSectionId);

        /// <summary>Получить дерево разделов </summary>
        [Get("/api/v1/Sections/tree")]
        Task<SectionTreeNode> GetSectionsAsTree([Query] long? parentSectionId);

    }
}
