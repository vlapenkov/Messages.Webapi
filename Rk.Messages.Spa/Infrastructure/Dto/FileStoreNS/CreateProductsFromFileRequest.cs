namespace Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS
{
    /// <summary>
    /// Запрос на создание файла
    /// </summary>
    public record CreateProductsFromFileRequest
    {
        public string FileName { get; set; }

        public byte[] Data { get; set; }
    }
}
