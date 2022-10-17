using Messages.Spa.Infrastructure.Dto.SectionsNS;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messages.Spa
{
    public interface ISectionsServices
    {
        /// <summary>Создать раздел</summary>         
        [Post("/api/v1/Sections")]
        Task<long> CreateSection([Body] CreateSectionRequest request);

        /// <summary>Получить список разделов </summary>
        [Get("/api/v1/Sections/list")]
        Task<IReadOnlyCollection<SectionDto>> GetSectionsAsList([Query] long? parentSectionId);

        /// <summary>Получить дерево разделов </summary>
        [Get("/api/v1/Sections/tree")]
        Task<SectionTreeNode> GetSectionsAsTree([Query] long? parentSectionId);

    }
}
