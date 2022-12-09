using MediatR;
using Rk.Messages.Logic.WorkProductsNS.Dto;
using System;

namespace Rk.Messages.Logic.WorkProductsNS.Commands.CreateWorkProduct
{
    public class CreateWorkProductCommand : IRequest<long>
    {
        public CreateWorkProductRequest Request { get; set; }
    }
}
