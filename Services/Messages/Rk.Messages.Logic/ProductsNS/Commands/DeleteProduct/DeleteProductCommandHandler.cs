using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : AsyncRequestHandler<DeleteProductCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public DeleteProductCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        protected override async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            await CheckIfProductHasLinks(command.ProductId);

            var productFound = await _appDbContext.Products.FirstOrDefaultAsync(self => self.Id == command.ProductId) ?? throw new EntityNotFoundException($"Продукция с Id= {command.ProductId} не найдена");


            _appDbContext.Products.Remove(productFound);

            await _appDbContext.SaveChangesAsync(cancellationToken);

        }

        private async Task CheckIfProductHasLinks(long productId)
        {
            bool hasCartItems = await _appDbContext.ShoppingCartItems.Where(self => self.ProductId == productId).AnyAsync();

            if (hasCartItems)
                throw new ValidationException(new[] { new ValidationFailure(nameof(productId), $"Продукция c Id={productId} есть в корзинах пользователей.") });

            bool hasOrderItems = await _appDbContext.OrderItems.Where(self => self.ProductId == productId).AnyAsync();
            if (hasOrderItems)
                throw new ValidationException(new[] { new ValidationFailure(nameof(productId), $"Продукция c Id={productId} есть в заказах пользователей.") });
        }
    }
}
