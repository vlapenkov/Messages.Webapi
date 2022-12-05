using AutoMapper;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.NewsNS.Dto;
using X.PagedList;

namespace Rk.Messages.Logic.NewsNS.Mappings
{
    public class NewsMappingProfile : Profile
    {
        public NewsMappingProfile()
        {
            CreateMap(typeof(IPagedList<News>), typeof(PagedResponse<NewsResponse>));

            CreateMap<News, NewsResponse>()
              .ReverseMap();
        }
    }
}
