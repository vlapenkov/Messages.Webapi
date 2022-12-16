using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.OrdersNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using X.PagedList;
using Rk.Messages.Domain.Entities;

namespace Rk.Messages.Logic.OrdersNS.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PagedResponse<OrderShortDto>>
    {
        private readonly IAppDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<PagedResponse<OrderShortDto>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var request = query.Request;

            IQueryable<Order> ordersQuery = _dbContext.Orders
                .Include(self => self.Organization)
                .Include(self => self.Producer)
                .Include(self => self.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .AsNoTracking();

            if (request.OrganisationName != null)
                ordersQuery = ordersQuery.Where(order => order.Organization.Name.ToLower().Contains(request.OrganisationName.ToLower()));

            if (request.OrganisationId != null)
                ordersQuery = ordersQuery.Where(order => order.OrganizationId == request.OrganisationId);

            if (request.ProducerName != null)
                ordersQuery = ordersQuery.Where(order => order.Producer.Name.ToLower().Contains(request.ProducerName.ToLower()));

            if (request.ProducerId != null)
                ordersQuery = ordersQuery.Where(order => order.ProducerId == request.ProducerId);

            if (request.UserName != null)
                ordersQuery = ordersQuery.Where(order => order.UserName.ToLower().Contains(request.UserName.ToLower()));
                        

            if (request.ProductName != null)
                ordersQuery = ordersQuery.Where(order => order.OrderItems.Select(io=>io.Product).Any(product=> product.Name.ToLower().Contains(request.ProductName.ToLower())));

            if (request.DateFrom != null)
                ordersQuery = ordersQuery.Where(order => order.Created >= request.DateFrom);

            if (request.DateTo != null)
                ordersQuery = ordersQuery.Where(order => order.Created < request.DateTo);


            IPagedList<Order> queryResult = await ordersQuery.OrderByDescending(order => order.Id).ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);


            // преобразование из IPagedList<Order> -> PagedResponse<OrderShortDto>

            var sourceList = _mapper.Map<IEnumerable<OrderShortDto>>(queryResult);

            var result = _mapper.Map<PagedResponse<OrderShortDto>>(queryResult);

            result.Rows = sourceList;

            return result;

        }
    }
}
