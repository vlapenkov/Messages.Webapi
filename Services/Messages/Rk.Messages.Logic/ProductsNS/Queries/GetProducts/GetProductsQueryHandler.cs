using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Interfaces.Interfaces.DAL;
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


      
        /// <summary>
        /// Получить все категории потомки текущей
        /// </summary>
        /// <param name="parentId">текущая категория</param>
        /// <returns></returns>
        private async Task<long[]> GetChildrenIds(long parentId)
        {
            List<long> childrenIds = new() { parentId};

            var ids =await _dbContext.CatalogSections.Where(self => self.ParentCatalogSectionId == parentId).Select(p => p.Id).ToArrayAsync();

            foreach (var id in ids) {
                childrenIds.AddRange(await GetChildrenIds(id));
            }

            return childrenIds.Distinct().ToArray();
        }


        public async Task<PagedResponse<ProductShortDto>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {

            var request = query.Request;

            IQueryable<Product> productsQuery = _dbContext.Products
                .Include(product => product.Organization)
                .Include(product=> product.ProductDocuments)
                .ThenInclude(pd => pd.Document)
                .AsNoTracking();

            if (request.CatalogSectionId != null)
            {
               var childrenIds = await GetChildrenIds(request.CatalogSectionId.Value);

                productsQuery = productsQuery.Where(product => childrenIds.Contains( product.CatalogSectionId));

            }

            if (request.Region != null)
                productsQuery = productsQuery.Where(product => product.Organization.Region!=null && product.Organization.Region.ToLower() == request.Region.ToLower());

            if (request.ProducerName != null)
                productsQuery = productsQuery.Where(product => product.Organization.Name != null && product.Organization.Name.ToLower() == request.ProducerName.ToLower());

            if (request.Name != null)
                productsQuery = productsQuery.Where(product => product.Name != null && product.Name.ToLower().Contains(request.Name.ToLower()));

               // productsQuery = productsQuery.Where(product => product.Name != null && product.Name == request.Name);


            IPagedList<Product> queryResult = await productsQuery.OrderBy(product=>product.Id).ToPagedListAsync(request.PageNumber, request.PageSize);


            // преобразование из IPagedList<Product> -> PagedResponse<ProductShortDto>

            var sourceList = _mapper.Map<IEnumerable<ProductShortDto>>(queryResult);

            var result = _mapper.Map<PagedResponse<ProductShortDto>>(queryResult);

            result.Rows = sourceList;

            return result;


        }
    }
}
