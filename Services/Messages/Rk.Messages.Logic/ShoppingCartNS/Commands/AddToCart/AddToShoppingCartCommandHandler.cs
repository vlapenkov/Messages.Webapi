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
    public class AddToShoppingCartCommandHandler : AsyncRequestHandler<AddToShoppingCartCommand>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IUserService _userService;

        public AddToShoppingCartCommandHandler(IAppDbContext appDbContext, IUserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
        }

        protected override async Task Handle(AddToShoppingCartCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            if (!_userService.IsAuthenticated) throw new RkErrorException("Пользователь не авторизован");

            var product = await _appDbContext.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ProductId)
                ?? throw new EntityNotFoundException($"Продукт не найден id={request.ProductId}");

            var cartItemFound = await _appDbContext.ShoppingCartItems.FirstOrDefaultAsync(self => self.UserName == _userService.UserName && self.ProductId == request.ProductId);


            if (cartItemFound != null)
            {
                cartItemFound.Increment(request.Quantity);
            }
            else
            {
                var cartItem = new ShoppingCartItem(_userService.UserName, product.Id, product.Price, request.Quantity);

                _appDbContext.ShoppingCartItems.Add(cartItem);
            }

            await _appDbContext.SaveChangesAsync(cancellationToken);
           
        }       
        
    }
}
