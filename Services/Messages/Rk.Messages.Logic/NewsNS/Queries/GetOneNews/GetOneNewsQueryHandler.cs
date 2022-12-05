using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.NewsNS.Dto;
using Rk.Messages.Logic.ProductsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.NewsNS.Queries.GetOneNews
{
    public class GetOneNewsQueryHandler : IRequestHandler<GetOneNewsQuery, NewsResponse>
    {
        private readonly IAppDbContext _appDbContext;

        private readonly IMapper _mapper;

        public GetOneNewsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<NewsResponse> Handle(GetOneNewsQuery request, CancellationToken cancellationToken)
        {
            var newsFound = await _appDbContext.News               
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == request.Id)
               ??
               throw new EntityNotFoundException($"Новость с Id = {request.Id} не найдена");


            var result = _mapper.Map<NewsResponse>(newsFound);

            return result;

        }
    }
}
