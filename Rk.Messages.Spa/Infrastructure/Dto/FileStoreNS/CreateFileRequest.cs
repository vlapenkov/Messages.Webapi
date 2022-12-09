namespace Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS
{
    /// <summary>
    /// Запрос на создание файла
    /// </summary>
    public record CreateFileRequest
    {
        public string FileName { get; set; }

        public byte[] Data{ get; set; }
    }
}
