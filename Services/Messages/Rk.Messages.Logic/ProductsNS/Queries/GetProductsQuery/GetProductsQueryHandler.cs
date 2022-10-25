using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.ProductsNS.Dto;
using X.PagedList;

namespace Rk.Messages.Logic.ProductsNS.Queries.GetProductsQuery
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedResponse<ProductShortDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;


        public GetProductsQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ProductShortDto>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {

            var request = query.Request;

            IQueryable<Product> productsQuery = _dbContext.Products.AsNoTracking();

            if (request.CatalogSectionId != null)
                productsQuery = productsQuery.Where(product => product.CatalogSectionId == request.CatalogSectionId);

            if (request.Name != null)
                productsQuery = productsQuery.Where(product => product.Name != null && product.Name.ToLower().Contains(request.Name.ToLower()));


            IPagedList<Product> queryResult = await productsQuery.OrderBy(product=>product.Id).ToPagedListAsync(request.PageNumber, request.PageSize);


            // преобразование из IPagedList<Product> -> PagedResponse<ProductShortDto>

            var sourceList = _mapper.Map<IEnumerable<ProductShortDto>>(queryResult);

            var result = _mapper.Map<PagedResponse<ProductShortDto>>(queryResult);

            result.Rows = sourceList;

            return result;


        }
    }
}
