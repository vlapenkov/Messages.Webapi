using AutoMapper;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Logic.ShoppingCartNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ShoppingCartNS.Mappings
{
    
        public class CartMappingProfile : Profile
        {
            public CartMappingProfile()
            {
                CreateMap<ShoppingCartItem, ShoppingCartItemDto>()
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                    .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.Product.GetProductDocument().Document.FileId))
                    .ReverseMap();
            }
        }
    
}
