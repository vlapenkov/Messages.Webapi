using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Interfaces.Services;
using Rk.Messages.Logic.ProductsNS.Dto;
using Rk.Messages.Logic.WorkProductsNS.Dto;
using Rk.Messages.Logic.WorkProductsNS.Queries.GetWorkProduct;

namespace Rk.Messages.Logic.ProductsNS.Queries.GetProductQuery
{
    public class GetWorkProductQueryHandler : IRequestHandler<GetWorkProductQuery, WorkProductResponse>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
      


        public GetWorkProductQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;            
        }
                
              

        public async Task<WorkProductResponse> Handle(GetWorkProductQuery request, CancellationToken cancellationToken)
        {
            byte[][] resultFiles = Array.Empty<byte[]>();

            var productFound = await _dbContext.WorkProducts
                // .Include(product => product.AttributeValues)
                 .Include(product => product.ProductDocuments)
                     .ThenInclude(pd => pd.Document)

                 .Include(product => product.Organization)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == request.Id)
                 ??
                 throw new EntityNotFoundException($"Продукция с Id = {request.Id} не найдена");


            var result = _mapper.Map<WorkProductResponse>(productFound);

            return result;
        }
    }
}
