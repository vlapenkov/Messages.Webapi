namespace Rk.Messages.Logic.ProductsNS.Dto
{
    public record CreateReviewRequest
    {
        public string Description { get; set; }

        public byte Rating { get; set; }
    }
}
