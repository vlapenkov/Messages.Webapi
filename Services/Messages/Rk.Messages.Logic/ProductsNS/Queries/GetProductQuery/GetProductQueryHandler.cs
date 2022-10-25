using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.ProductsNS.Dto;

namespace Rk.Messages.Logic.ProductsNS.Queries.GetProductQuery
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResponse>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;


        public GetProductQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }        

        public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var productFound = await _dbContext.Products
                .Include(product=> product.AttributeValues)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id) 
                ?? 
                throw new EntityNotFoundException($"Продукция с Id = {request.Id} не найдена");

            var result = _mapper.Map<ProductResponse>(productFound);

            return result;
        }
    }
}
