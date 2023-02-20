namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    public record CreateReviewRequest
    {
        public string Description { get; set; }

        public byte Rating { get; set; }
    }
}
