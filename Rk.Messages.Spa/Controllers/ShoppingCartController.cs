using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.ShoppingCartNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    /// <summary>
    /// Управление корзиной
    /// </summary>    
    
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        /// <summary>Добавить товар в корзину</summary>        
        [HttpPost]
        public async Task<long> AddToCart([FromBody] AddToShoppingCartRequest request)
        {
            return await _shoppingCartService.AddToCart(request);
        }

        /// <summary>Получить товары в корзине</summary>        
        [HttpGet]
        public async Task<IReadOnlyCollection<ShoppingCartItemDto>> GetCartItems()
        {

            return await _shoppingCartService.GetCartItems();
        }
    }
}
