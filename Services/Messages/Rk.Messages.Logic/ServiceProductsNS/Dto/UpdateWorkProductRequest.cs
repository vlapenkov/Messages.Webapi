namespace Rk.Messages.Logic.ServiceProductsNS.Dto
{
    /// <summary>
    /// Создание запроса услуги
    /// </summary>
    public record UpdateServiceProductRequest
    {
        public long CatalogSectionId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }
       
    }
}
