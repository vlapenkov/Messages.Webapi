using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.NewsNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    /// <summary>
    /// Сервис для работы с новостями
    /// </summary>
    public interface INewsService
    {
        /// <summary>Создать новость</summary>         
        [Post("/api/v1/News")]
        Task<long> CreateNews([Body] CreateNewsRequest request);
       

        /// <summary>Получить инфо о новости</summary>  
        [Get("/api/v1/News/{id}")]
        Task<NewsResponse> GetNews(long id);

        /// <summary>Получить список новостей</summary>  
        [Get("/api/v1/News")]
        Task<PagedResponse<NewsResponse>> GetNews();

        /// <summary>Удалить новость</summary>  
        [Delete("/api/v1/News/{id}")]
        Task DeleteById(long id);

    }
}
