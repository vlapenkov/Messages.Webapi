using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.ServiceProductsNS.Dto;

namespace Rk.Messages.Logic.ServiceProductsNS.Queries.GetServiceProduct
{
    public class GetServiceProductQueryHandler : IRequestHandler<GetServiceProductQuery, ServiceProductResponse>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public GetServiceProductQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;            
        }
        
        public async Task<ServiceProductResponse> Handle(GetServiceProductQuery request, CancellationToken cancellationToken)
        {
            byte[][] resultFiles = Array.Empty<byte[]>();

            var productFound = await _dbContext.ServiceProducts
                // .Include(product => product.AttributeValues)
                 .Include(product => product.ProductDocuments)
                     .ThenInclude(pd => pd.Document)

                 .Include(product => product.Organization)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
                 ??
                 throw new EntityNotFoundException($"Услуга с Id = {request.Id} не найдена");


            var result = _mapper.Map<ServiceProductResponse>(productFound);

            return result;
        }
    }
}
