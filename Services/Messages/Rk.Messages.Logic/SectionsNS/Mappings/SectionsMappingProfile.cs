using AutoMapper;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Logic.SectionsNS.Dto;
using System.Linq;

namespace Rk.Messages.Logic.SectionsNS.Mappings
{
    public class SectionsMappingProfile : Profile
    {
        public SectionsMappingProfile()
        {
            CreateMap<CatalogSection, SectionDto>()
                .ForMember(dest => dest.ParentSectionId, opt => opt.MapFrom(src => src.ParentCatalogSectionId))
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.SectionDocuments.FirstOrDefault().Document.FileId))
                .ReverseMap();
        }
    }
}
