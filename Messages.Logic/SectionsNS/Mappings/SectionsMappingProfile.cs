using AutoMapper;
using Messages.Domain.Models;
using Messages.Logic.SectionsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Logic.SectionsNS.Mappings
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
