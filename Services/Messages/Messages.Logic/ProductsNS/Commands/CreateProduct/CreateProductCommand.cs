using MediatR;
using Messages.Logic.ProductsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Logic.ProductsNS.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<long>
    {
        public CreateProductRequest Request { get; set; }
    }  
}
