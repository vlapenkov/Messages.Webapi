using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ShoppingCartNS.Commands.DeleteFromCart
{
    public class DeleteFromCartCommandHandler : AsyncRequestHandler<DeleteFromCartCommand>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IUserService _userService;

        public DeleteFromCartCommandHandler(IAppDbContext appDbContext, IUserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
        }

        protected override async Task Handle(DeleteFromCartCommand request, CancellationToken cancellationToken)
        {
            if (!_userService.IsAuthenticated) throw new RkErrorException("Пользователь не авторизован");

            var cartItemFound = await _appDbContext.ShoppingCartItems.FirstOrDefaultAsync(self => self.UserName == _userService.UserName && self.ProductId == request.ProductId)
                  ??
                  throw new EntityNotFoundException($"Продукция не найдена Id={request.ProductId}"); 

            _appDbContext.ShoppingCartItems.Remove(cartItemFound);

            await _appDbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
