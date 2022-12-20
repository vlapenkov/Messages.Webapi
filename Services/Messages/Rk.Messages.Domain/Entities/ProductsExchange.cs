using Rk.Messages.Domain.Enums;

namespace Rk.Messages.Domain.Entities
{
    public class ProductsExchange : AuditableEntity
    {
        public ProductsExchange(ProductExchangeType exchangeType, long productsLoaded)
        {
            ExchangeType = exchangeType;
            ProductsLoaded = productsLoaded;
        }

        public ProductExchangeType ExchangeType { get; protected set; }

        public long ProductsLoaded { get; protected set; }
    }
}
