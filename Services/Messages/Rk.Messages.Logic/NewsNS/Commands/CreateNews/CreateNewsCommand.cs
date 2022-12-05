using MediatR;
using Rk.Messages.Logic.NewsNS.Dto;
using Rk.Messages.Logic.ProductsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.NewsNS.Commands.CreateNews
{
    public class CreateNewsCommand : IRequest<long>
    {
        public CreateNewsRequest Request { get; set; }
    }
}
