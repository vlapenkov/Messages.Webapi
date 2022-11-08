using AutoMapper;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using Rk.Messages.Logic.OrdersNS.Dto;

namespace Rk.Messages.Logic.OrdersNS.Mappings
{
    public class OrdersMappingProfile : Profile
    {
        public OrdersMappingProfile()
        {

            //CreateMap(typeof(IPagedList<Product>), typeof(PagedResponse<ProductShortDto>));
            //TODO: Разобраться с mapping для Rows
            // .ForMember(dest => dest.Rows, opt => opt.MapFrom(src => src.Name))

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
               .ReverseMap();

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.OrganisationName, opt => opt.MapFrom(src => src.Organization.FullName))                
                .ReverseMap();
            
        }
    }
}
