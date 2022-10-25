using AutoMapper;
using Rk.Messages.Logic.SectionsNS.Dto;

namespace Rk.Messages.Logic.SectionsNS.Mappings
{
    public class SectionsMappingProfile : Profile
    {
        public SectionsMappingProfile()
        {
            CreateMap<CatalogSection, SectionDto>()
                .ForMember(dest => dest.ParentSectionId, opt => opt.MapFrom(src => src.ParentCatalogSectionId))
                .ReverseMap();
        }
    }
}
