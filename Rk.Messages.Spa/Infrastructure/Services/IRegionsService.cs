using Refit;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    /// <summary>
    /// Сервис для работы с регионами
    /// </summary>
    public interface IRegionsService
    {

        [Get("/api/v1/Regions")]
        Task<IReadOnlyCollection<string>> GetRegions();

    }
}
