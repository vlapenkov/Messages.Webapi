using AutoMapper;
using Messages.Domain.Models;
using Messages.Domain.Models.Products;
using Messages.Logic.CommonNS.Dto;
using Messages.Logic.ProductsNS.Dto;
using Messages.Logic.SectionsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Messages.Logic.ProductsNS.Mappings
{
    public class ProductsMappingProfile : Profile
    {
        public ProductsMappingProfile()
        {

            CreateMap(typeof(IPagedList<Product>), typeof(PagedResponse<ProductShortDto>));
            //TODO: Разобраться с mapping для Rows
            // .ForMember(dest => dest.Rows, opt => opt.MapFrom(src => src.Name))

            CreateMap<AttributeValue, AttributeValueDto>()
               .ReverseMap();

            CreateMap<Product, ProductShortDto>()                
                .ReverseMap();

            CreateMap<Product, ProductResponse>()              
              .ReverseMap();
        }
    }
}
