using MediatR;
using Rk.Messages.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Commands.SetStatus
{
    /// <summary>
    /// Изменить статус продукции
    /// </summary>
    public class SetStatusCommand :IRequest
    {
        public long ProductId { get; set; }

        public ProductStatus Status { get; set; }
    }
}
