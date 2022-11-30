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

namespace Rk.Messages.Logic.ProductsNS.Queries.GetProductQuery
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResponse>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileStoreService _fileService;


        public GetProductQueryHandler(IAppDbContext dbContext, IMapper mapper, IFileStoreService fileService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
        }
                

        public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            byte[][] resultFiles = Array.Empty<byte[]>(); 

           var productFound = await _dbContext.Products
                .Include(product=> product.AttributeValues)
                .Include(product => product.ProductDocuments)
                    .ThenInclude(pd=>pd.Document)

                .Include(product => product.Organization)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id) 
                ?? 
                throw new EntityNotFoundException($"Продукция с Id = {request.Id} не найдена");
            

            var result = _mapper.Map<ProductResponse>(productFound);

            return result;
        }
    }
}
