﻿using AutoMapper;
using Elasticsearch.Net;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.ProductsNS.Dto;
using System;
using System.Linq;
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
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.ProductDocuments.FirstOrDefault().Document.FileId))
                .ReverseMap();

             CreateMap<Product, ProductResponse>()
               .ForMember(dest => dest.Documents, opt => opt.Ignore())
              .ReverseMap();
        }
    }
}
