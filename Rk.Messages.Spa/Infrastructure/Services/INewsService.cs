using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.NewsNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    /// <summary>
    /// Сервис для работы с продукцией
    /// </summary>
    public interface INewsService
    {
        /// <summary>Создать новость</summary>         
        [Post("/api/v1/News")]
        Task<long> CreateNews([Body] CreateNewsRequest request);
       

        /// <summary>Получить инфо о продукции</summary>  
        [Get("/api/v1/News/{id}")]
        Task<NewsResponse> GetNews(long id);

       
    }
}
