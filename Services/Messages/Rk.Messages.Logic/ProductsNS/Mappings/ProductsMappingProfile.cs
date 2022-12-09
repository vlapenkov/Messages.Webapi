using AutoMapper;
using Rk.Messages.Common.Helpers;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.ProductsNS.Dto;
using Rk.Messages.Logic.WorkProductsNS.Dto;
using System.Linq;
using Rk.Messages.Logic.ServiceProductsNS.Dto;
using X.PagedList;

namespace Rk.Messages.Logic.ProductsNS.Mappings
{
    public class ProductsMappingProfile : Profile
    {
        public ProductsMappingProfile()
        {

            CreateMap(typeof(IPagedList<Product>), typeof(PagedResponse<ProductShortDto>));

            CreateMap(typeof(IPagedList<BaseProduct>), typeof(PagedResponse<ProductShortDto>));
            //TODO: Разобраться с mapping для Rows
            // .ForMember(dest => dest.Rows, opt => opt.MapFrom(src => src.Name))

            CreateMap<AttributeValue, AttributeValueDto>()
               .ReverseMap();

            CreateMap<BaseProduct, ProductShortDto>()
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.ProductDocuments.FirstOrDefault().Document.FileId))
                 .ForMember(dest => dest.StatusText, opt => opt.MapFrom(src => src.Status.GetDescription()))
                 .ForMember(dest => dest.ProductionType, opt => opt.MapFrom(src => src.GetProductionType()))
                 //.ForMember(dest => dest.AvailableStatusText, opt => opt.MapFrom(src => src.AvailableStatus.GetDescription()))
                 .ForMember(dest => dest.AvailableStatusText, opt => opt.Ignore())
                .ReverseMap();

             CreateMap<Product, ProductResponse>()

               .ForMember(dest => dest.StatusText, opt => opt.MapFrom(src => src.Status.GetDescription()))
               .ForMember(dest => dest.AvailableStatusText, opt => opt.MapFrom(src => src.AvailableStatus.GetDescription()))
               .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.ProductDocuments.Select(pd=>pd.Document)))
              .ReverseMap();

            CreateMap<WorkProduct, WorkProductResponse>()
              .ForMember(dest => dest.StatusText, opt => opt.MapFrom(src => src.Status.GetDescription()))
              .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.ProductDocuments.Select(pd => pd.Document)))
             .ReverseMap();

            CreateMap<ServiceProduct, ServiceProductResponse>()
                .ForMember(dest => dest.StatusText, opt => opt.MapFrom(src => src.Status.GetDescription()))
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.ProductDocuments.Select(pd => pd.Document)))
                .ReverseMap();

            CreateMap<Organization, OrganizationShortDto>()                
                .ReverseMap();

            CreateMap<ProductAttribute, AttributeDto>()
              .ReverseMap();

            CreateMap<Document, FileDataDto>()
              .ReverseMap();

        }
    }
}
