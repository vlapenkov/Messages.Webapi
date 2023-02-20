using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.ServiceProductNS
{
    /// <summary>
    /// Создание запроса услуги
    /// </summary>
    public record CreateServiceProductRequest
    {
        public long CatalogSectionId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }       

        public decimal? Price { get; set; }
        
        public bool? AreForeignComponentsUsed { get; set; }

        /// <summary>Информация о файлах для продукции</summary>
        public List<FileDataDto> Documents { get; set; } = new List<FileDataDto>();

    }
}
