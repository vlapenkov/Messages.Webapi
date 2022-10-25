using AutoMapper;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.ProductsNS.Dto;
using X.PagedList;

namespace Rk.Messages.Logic.ProductsNS.Mappings
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
