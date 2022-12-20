using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.ProductsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Queries.GetProductsExchanges
{
    public class GetProductsExchangesQueryHandler : IRequestHandler<GetProductsExchangesQuery, IReadOnlyCollection<ProductsExchangeDto>>
    {
        private readonly IAppDbContext _appDbContext;
        
        private readonly IMapper _mapper;
                

        public GetProductsExchangesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<ProductsExchangeDto>> Handle(GetProductsExchangesQuery request, CancellationToken cancellationToken)
        {
           var result =  await _appDbContext.ProductsExchanges
                .ProjectTo<ProductsExchangeDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(self=>self.Id)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
