using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrdersNS.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, long>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IUserService _userService;
        private static readonly string _inn = "inn";

     

        public CreateOrderCommandHandler(IAppDbContext appDbContext, IUserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
        }

        public async Task<long> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {

            if (!_userService.IsAuthenticated) throw new RkUnauthorizedAccessException("Пользователь не авторизован");



            var cartItemsFound = await _appDbContext.ShoppingCartItems.Where(self => self.UserName == _userService.UserName).ToListAsync();

            if (!cartItemsFound.Any()) throw new RkErrorException($"Не найдено ни одной позиции в корзине для пользователя {_userService.UserName}");

            Organization organisationFound = await GetOrganizationByUser();

            var order = new Order(organisationFound.Id, _userService.UserName);

            var orderItems = cartItemsFound.Select(cartItem => new OrderItem(cartItem.ProductId, cartItem.Price, cartItem.Quantity)).ToList();

            order.AddOrderItems(orderItems);

            // создаем заказ
            _appDbContext.Orders.Add(order);

            // удаляем позиции из корзины
            _appDbContext.ShoppingCartItems.RemoveRange(cartItemsFound);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return order.Id;

        }

        /// <summary>
        /// Получить организацию из инн пользователя
        /// </summary>        
        private async Task<Organization> GetOrganizationByUser()
        {
            string inn = _userService.GetClaimValue(_inn) ?? throw new RkErrorException("У текущего пользователя не установлен ИНН");

            var organisationFound = await _appDbContext.Organizations.AsNoTracking().FirstOrDefaultAsync(x => x.Inn == inn)
                ??
                throw new EntityNotFoundException($"Организация не найдена по ИНН={inn}");
            return organisationFound;
        }
    }
}
