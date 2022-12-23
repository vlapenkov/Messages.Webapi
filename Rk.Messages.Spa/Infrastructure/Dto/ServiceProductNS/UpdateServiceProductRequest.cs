namespace Rk.Messages.Spa.Infrastructure.Dto.ServiceProductNS
{
    /// <summary>
    /// Изменение услуги
    /// </summary>
    public record UpdateServiceProductRequest
    {
        public long CatalogSectionId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }       

        public decimal? Price { get; set; }        
        
        public bool? AreForeignComponentsUsed { get; set; }

    }
}
