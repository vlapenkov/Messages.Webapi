using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Commands.AddReview
{
    public class AddReviewCommandHandler : AsyncRequestHandler<AddReviewCommand>
    {
        private readonly IAppDbContext _dbContext;

        public AddReviewCommandHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(AddReviewCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            var productFound = await _dbContext.BaseProduct
                .Include(self => self.Reviews)
                .FirstOrDefaultAsync(self => self.Id == command.ProductId) ?? throw new EntityNotFoundException($"Продукция с Id= {command.ProductId} не найдена");


            productFound.AddReview(new Review(command.ProductId, request.Description, request.Rating));

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
