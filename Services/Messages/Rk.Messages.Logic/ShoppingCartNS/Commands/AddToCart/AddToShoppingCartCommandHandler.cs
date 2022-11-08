using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

namespace Rk.Messages.Logic.ShoppingCartNS.Commands.AddToShoppingCartCommand
{
    public class AddToShoppingCartCommandHandler : IRequestHandler<AddToShoppingCartCommand, long>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IUserService _userService;

        public AddToShoppingCartCommandHandler(IAppDbContext appDbContext, IUserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
        }

        public async  Task<long> Handle(AddToShoppingCartCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            if (!_userService.IsAuthenticated) throw new RkErrorException("Пользователь не авторизован");

            var product = await _appDbContext.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ProductId)
                ?? throw new EntityNotFoundException($"Продукт не найден id={request.ProductId}");

           var cartItem = new ShoppingCartItem(_userService.UserName, product.Id, product.Price, request.Quantity);

            _appDbContext.ShoppingCartItems.Add(cartItem);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return cartItem.Id;
        }

    }
}
