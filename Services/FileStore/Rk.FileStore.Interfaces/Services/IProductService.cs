using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.FileStore.Interfaces.Services
{
    /// <summary>
    /// Сервис для генерации данных о продукции
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Получить данные о продукции
        /// </summary>        
       Task<IReadOnlyCollection<ProductDto>> GenerateProductData(byte[] data);
    }
}
