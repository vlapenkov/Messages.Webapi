using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Domain.Enums;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.ProductsNS.Dto;
using X.PagedList;

namespace Rk.Messages.Logic.ProductsNS.Queries.GetProductsQuery
{
    /// <summary>
    /// Обработчик возвращающий всю продукцию по критериям поиска
    /// </summary>
    public class GetProductionsQueryHandler : IRequestHandler<GetProductionsQuery, PagedResponse<ProductShortDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;


        public GetProductionsQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }



        public async Task<PagedResponse<ProductShortDto>> Handle(GetProductionsQuery query, CancellationToken cancellationToken)
        {

            var request = query.Request;

            IQueryable<BaseProduct> productsQuery = GetProductions();

            productsQuery = await FilterProducts(request, productsQuery);

            IPagedList<BaseProduct> queryResult = await productsQuery.ToPagedListAsync(request.PageNumber, request.PageSize);


            // преобразование из IPagedList<Product> -> PagedResponse<ProductShortDto>

            var sourceList = _mapper.Map<IEnumerable<ProductShortDto>>(queryResult);

            var result = _mapper.Map<PagedResponse<ProductShortDto>>(queryResult);

            result.Rows = sourceList;

            return result;


        }

        /// <summary>Получить товары со связанными сущностями</summary>
        
        private IQueryable<BaseProduct> GetProductions()
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

            switch (request.OrderBy)
            {
                case OrderByProduct.NameByAsc:
                    productsQuery = productsQuery.OrderBy(product => product.Name);
                    break;
                case OrderByProduct.NameByDesc:
                    productsQuery = productsQuery.OrderByDescending(product => product.Name);
                    break;
                case OrderByProduct.RegionByAsc:
                    productsQuery = productsQuery.OrderBy(product => product.Organization.Region);
                    break;
                case OrderByProduct.RegionByDesc:
                    productsQuery = productsQuery.OrderByDescending(product => product.Organization.Region);
                    break;
                case OrderByProduct.ProducerByAsc:
                    productsQuery = productsQuery.OrderBy(product => product.Organization.Name);
                    break;
                case OrderByProduct.ProducerByDesc:
                    productsQuery = productsQuery.OrderByDescending(product => product.Organization.Name);
                    break;
                case OrderByProduct.RatingByAsc:
                    {                    
                    productsQuery = productsQuery.OrderBy(product => product.Rating ?? -1);
                    }
                    break;
                case OrderByProduct.RatingByDesc:
                    productsQuery = productsQuery.OrderByDescending(product => product.Rating ?? -1);
                    break;

                case OrderByProduct.IdByAsc:
                    productsQuery = productsQuery.OrderBy(product => product.Id);
                    break;
                case OrderByProduct.IdByDesc:
                    productsQuery = productsQuery.OrderByDescending(product => product.Id);
                    break;

                default:
                    productsQuery = productsQuery.OrderBy(product => product.Name);
                    break;
            }

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
