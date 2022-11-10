using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.OrdersNS.Dto;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrdersNS.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderResponse>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var orderFound = await _appDbContext.Orders
                .Include(order=>order.Organization)
                .Include(order => order.OrderItems)
                    .ThenInclude(orderItem => orderItem.Product)
                .FirstOrDefaultAsync(x => x.Id == request.OrderId) 
                ?? 
                throw new EntityNotFoundException($"Заказ с Id= {request.OrderId} не найден");

            var result =_mapper.Map<OrderResponse>(orderFound);

            return result;
        }
    }
}
