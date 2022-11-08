using MediatR;
using Rk.Messages.Logic.ShoppingCartNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ShoppingCartNS.Commands.AddToShoppingCartCommand
{
    /// <summary>
    /// Команда добавить в корзину
    /// </summary>
    public class AddToShoppingCartCommand : IRequest
    {
        public AddToShoppingCartRequest Request { get; set; }
    }
}
