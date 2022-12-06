using OfficeOpenXml;
using Rk.FileStore.Interfaces.Services;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;

namespace Rk.FileStore.Infrastructure.Services
{
    /// <inheritdoc cref="IProductService"/>
    public class ProductService : IProductService
    {
        private enum _productColumns { Name = 1, FullName, Description, Weight, Price, Url  };

        const byte _initialRow = 2, _defaultSectionId = 11;

        private readonly IRemoteImageService _remoteImageService;       
        

        public ProductService(IRemoteImageService remoteImageService)
        {
            _remoteImageService = remoteImageService;
        }

        /// <summary>
        /// Получить значение в ячейке в виде строки
        /// </summary>        
        private string GetValue(ExcelWorksheet workSheet, int row, _productColumns column) => GetValue(workSheet, row, (int)column);


        /// <summary>
        /// Получить значение в ячейке в виде строки
        /// </summary>   
        private string GetValue(ExcelWorksheet workSheet, int row, int column)
        {

            if (workSheet == null) throw new ArgumentNullException(nameof(workSheet));

            if (workSheet.Cells == null) throw new ArgumentNullException(nameof(workSheet.Cells));

            return workSheet.Cells[row, column].Value?.ToString();
        }

        /// <summary>
        /// Получить данные продукции
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<ProductDto>> GenerateProductData(byte[] data)
        {
            var products = new List<ProductDto>();

            using (var stream = new MemoryStream(data))

            using (ExcelPackage package = new ExcelPackage(stream))
            {
                // Берем первую страницу
                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];

                int totalRows = workSheet.Dimension.Rows;

                // Начинаем со второй row
                for (int row = _initialRow; row <= totalRows; row++)
                {

                    var productDto = new ProductDto()
                    {
                        CatalogSectionId = _defaultSectionId,
                        Name = GetValue(workSheet, row, _productColumns.Name),
                        FullName = GetValue(workSheet, row, _productColumns.FullName),
                        Description = GetValue(workSheet, row, _productColumns.Description)
                    };


                    var priceStr = GetValue(workSheet, row, _productColumns.Price);

                    if (Decimal.TryParse(priceStr, out decimal price)) productDto.Price = price;


                    string url = GetValue(workSheet, row, _productColumns.Url);

                    if (!string.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        var fileData = await _remoteImageService.GetFile(new Uri(url));

                        if (fileData != null)
                            productDto.Documents.Add(fileData);
                    }

                    products.Add(productDto);
                }

            }
            return products;
        }
    }
}



