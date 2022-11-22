namespace Rk.FileStore.Webapi.Models
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
