using Microsoft.AspNetCore.Mvc;
using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    public interface IProductsPrepareService
    {
        /// <summary>
        /// Сгенерировать данные для товаров из excel
        /// </summary>
        /// <param name="request">excel в base64</param>
        /// <returns>данные для товаров</returns>
        [Post("/api/v1/productsprepare")]
        Task<IReadOnlyCollection<CreateProductRequest>> PrepareProductsFromExcel([FromBody] CreateProductsFromFileRequest request);


    }
    
}
