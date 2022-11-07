using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using Rk.Messages.Logic.ShoppingCartNS.Commands.AddToShoppingCartCommand;
using Rk.Messages.Logic.ShoppingCartNS.Dto;
using Rk.Messages.Logic.ShoppingCartNS.Queries.GetCartItems;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rk.Messages.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public ShoppingCartController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>Добавить товар в корзину</summary>        
        [HttpPost]
        public async Task<long> AddToCart([FromBody] AddToShoppingCartRequest request)
        {

            return await _mediatr.Send(new AddToShoppingCartCommand {Request = request });
        }

        /// <summary>Получить товары в корзине</summary>        
        [HttpGet]
        public async Task<IReadOnlyCollection<ShoppingCartItemDto>> GetCartItems()
        {

            return await _mediatr.Send(new GetCartItemsQuery { });
        }
    }
}
