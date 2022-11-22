using Microsoft.AspNetCore.Mvc;
using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.ShoppingCartNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    public interface IShoppingCartService
    {
        /// <summary>Создать продукцию</summary>         
        [Post("/api/v1/ShoppingCart")]
        Task AddToCart([Body] AddToShoppingCartRequest request);

        /// <summary>Получить пагинированный список продукции</summary>  
        [Get("/api/v1/ShoppingCart")]
        Task<IReadOnlyCollection<ShoppingCartItemDto>> GetCartItems();

        /// <summary>Удалить строку из корзины</summary>  
        [Delete("/api/v1/ShoppingCart/{productId}")]
        Task DeleteFromCart(long productId);
    }
}
