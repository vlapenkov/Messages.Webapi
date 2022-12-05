using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.NewsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Rk.Messages.Logic.NewsNS.Queries.GetNews
{
    public class GetAllNewsQueryHandler : IRequestHandler<GetAllNewsQuery, PagedResponse<NewsResponse>>
    {
        private readonly IAppDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetAllNewsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<PagedResponse<NewsResponse>> Handle(GetAllNewsQuery query, CancellationToken cancellationToken)
        {

            var request = query.Request;

            IQueryable<News> newsQuery = _dbContext.News.AsNoTracking();

            IPagedList<News> queryResult = await newsQuery.OrderBy(order => order.Id).ToPagedListAsync(request.PageNumber, request.PageSize);


            var sourceList = _mapper.Map<IEnumerable<NewsResponse>>(queryResult);

            var result = _mapper.Map<PagedResponse<NewsResponse>>(queryResult);

            result.Rows = sourceList;

            return result;
        }
    }
}
