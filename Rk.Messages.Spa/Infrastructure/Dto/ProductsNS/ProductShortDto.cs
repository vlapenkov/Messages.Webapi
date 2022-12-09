using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    /// <summary>
    /// Информация о товаре для списка
    /// </summary>
    /// <summary>
    /// Информация о товаре для списка
    /// </summary>
    public record ProductShortDto : AuditableEntityDto
    {       

        /// <summary>наименование продукции</summary>
        public string Name { get; set; }

        /// <summary>описание продукции</summary>
        public string Description { get; set; }

        /// <summary>статус</summary>
        public string StatusText { get; set; }

        /// <summary>статус доступности</summary>
        public string AvailableStatusText { get; set; }

        /// <summary>цена</summary>
        public decimal Price { get; set; }


        /// <summary>Тип продукции</summary>
        public string ProductionType { get; set; }

        /// <summary>Ссылка на документ</summary>
        public Guid? DocumentId { get; set; }

        public  OrganizationShortDto Organization { get; set; }

    }
}
