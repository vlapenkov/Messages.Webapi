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
using Rk.Messages.Logic.CommonNS.Dto;

namespace Rk.Messages.Logic.OrdersNS.Mappings
{
    public class OrdersMappingProfile : Profile
    {
        public OrdersMappingProfile()
        {

            CreateMap(typeof(IPagedList<Order>), typeof(PagedResponse<OrderShortDto>));
            

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
               .ReverseMap();

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.OrganisationName, opt => opt.MapFrom(src => src.Organization.FullName))                
                .ReverseMap();

            CreateMap<Order, OrderShortDto>()
              .ForMember(dest => dest.OrganisationName, opt => opt.MapFrom(src => src.Organization.FullName))
              .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.OrderItems.Sum(oi=>oi.Sum)))
              .ReverseMap();

        }
    }
}
