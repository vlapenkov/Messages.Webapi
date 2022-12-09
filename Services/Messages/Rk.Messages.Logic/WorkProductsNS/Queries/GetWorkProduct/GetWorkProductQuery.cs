using MediatR;
using Rk.Messages.Logic.WorkProductsNS.Dto;

namespace Rk.Messages.Logic.WorkProductsNS.Queries.GetWorkProduct
{
    public class GetWorkProductQuery : IRequest<WorkProductResponse>
    {
        public long Id { get; set; }
    }
}
