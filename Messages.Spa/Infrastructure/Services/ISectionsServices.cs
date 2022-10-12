using Messages.Spa.Infrastructure.Dto.SectionsNS;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messages.Spa
{
    public interface ISectionsServices
    {
        [Post("/api/v1/Sections")]
        Task<long> CreateSection([Body] CreateSectionRequest request);

        [Get("/api/v1/Sections")]
        Task<IReadOnlyCollection<SectionDto>> GetSections([Query] long? parentSectionId);

    }
}
