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
    /// <summary>
    /// Обработчик возвращающий всю продукцию по критериям поиска
    /// </summary>
    public class GetProductionsQueryHandler : IRequestHandler<GetProductsQuery, PagedResponse<ProductShortDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;


        public GetProductionsQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }



        public async Task<PagedResponse<ProductShortDto>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {

            var request = query.Request;

            IQueryable<BaseProduct> productsQuery = GetProducts();

            productsQuery = await FilterProducts(request, productsQuery);

            IPagedList<BaseProduct> queryResult = await productsQuery.OrderBy(product => product.Id).ToPagedListAsync(request.PageNumber, request.PageSize);


            // преобразование из IPagedList<Product> -> PagedResponse<ProductShortDto>

            var sourceList = _mapper.Map<IEnumerable<ProductShortDto>>(queryResult);

            var result = _mapper.Map<PagedResponse<ProductShortDto>>(queryResult);

            result.Rows = sourceList;

            return result;


        }

        /// <summary>Получить товары со связанными сущностями</summary>
        
        private IQueryable<BaseProduct> GetProducts()
        {
            return _dbContext.BaseProduct
                .Include(product => product.Organization)
                .Include(product => product.ProductDocuments)
                .ThenInclude(pd => pd.Document)
                .AsNoTracking();
        }

        /// <summary>Отобрать товары по фильтру</summary>
        private async Task<IQueryable<BaseProduct>> FilterProducts(FilterProductsRequest request, IQueryable<BaseProduct> productsQuery)
        {
            if (request.CatalogSectionId != null)
            {
                var childrenIds = await GetChildrenIds(request.CatalogSectionId.Value);

                productsQuery = productsQuery.Where(product => childrenIds.Contains(product.CatalogSectionId));

            }

            if (request.Status != null)
            {
                productsQuery = productsQuery.Where(product => product.Status == request.Status);
            }

            //if (request.AvailableStatus != null)
            //{
            //    productsQuery = productsQuery.Where(product => product.AvailableStatus == request.AvailableStatus);
            //}

            if (request.Region != null)
                productsQuery = productsQuery.Where(product => product.Organization.Region != null && product.Organization.Region.ToLower() == request.Region.ToLower());

            if (request.ProducerId != null)
                productsQuery = productsQuery.Where(product => product.OrganizationId == request.ProducerId);

            if (request.ProducerName != null)
                productsQuery = productsQuery.Where(product => product.Organization.Name != null && product.Organization.Name.ToLower() == request.ProducerName.ToLower());

            if (request.Name != null)
                productsQuery = productsQuery.Where(product => product.Name != null && product.Name.ToLower().Contains(request.Name.ToLower()));
            return productsQuery;
        }

        /// <summary>
        /// Получить все категории потомки текущей
        /// </summary>
        /// <param name="parentId">текущая категория</param>
        /// <returns></returns>
        private async Task<long[]> GetChildrenIds(long parentId)
        {
            List<long> childrenIds = new() { parentId };

            var ids = await _dbContext.CatalogSections.Where(self => self.ParentCatalogSectionId == parentId).Select(p => p.Id).ToArrayAsync();

            foreach (var id in ids)
            {
                childrenIds.AddRange(await GetChildrenIds(id));
            }

            return childrenIds.Distinct().ToArray();
        }
    }
}
