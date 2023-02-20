using MediatR;
using Rk.Messages.Logic.ProductsNS.Dto;

namespace Rk.Messages.Logic.ProductsNS.Commands.AddReview
{
    /// <summary>
    /// Команда добавления отзыва
    /// </summary>
    public class AddReviewCommand : IRequest
    {
        public long ProductId { get; set; }

        public CreateReviewRequest Request { get; set; }
    }
}
