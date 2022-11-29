
namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    /// <summary>
    /// Значение атрибута товара
    /// </summary>
    public record AttributeValueDto
    {
        /// <summary>Id товара</summary>
       // public long BaseProductId { get; set; }

        /// <summary>Id атрибута</summary>
        public long AttributeId { get; set; }

        /// <summary>Значение атрибута</summary>
        public string Value { get;  set; }

    }
}