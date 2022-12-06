using Rk.FileStore.Interfaces.Dto;

namespace Rk.FileStore.Interfaces
{ 
    /// <summary>
    /// Запрос на создание файла
    /// </summary>
    public record CreateProductsFromFileRequest : IFileData
    {
        public string FileName { get; set; }

        public byte[] Data{ get; set; }
    }
}
