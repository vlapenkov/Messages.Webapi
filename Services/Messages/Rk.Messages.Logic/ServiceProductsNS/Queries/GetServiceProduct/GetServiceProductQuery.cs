using MediatR;
using Rk.Messages.Logic.ServiceProductsNS.Dto;

namespace Rk.Messages.Logic.ServiceProductsNS.Queries.GetServiceProduct
{
    public class GetServiceProductQuery : IRequest<ServiceProductResponse>
    {
        public long Id { get; set; }
    }
}
