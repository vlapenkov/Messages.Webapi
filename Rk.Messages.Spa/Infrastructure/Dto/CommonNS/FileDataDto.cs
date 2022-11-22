namespace Rk.Messages.Spa.Infrastructure.Dto.CommonNS
{
    /// <summary>
    /// Информация о файлах для продукции
    /// </summary>
    public record FileDataDto
    {
        /// <summary>Имя файла</summary>
        public string? FileName { get; set; }

        /// <summary>Данные</summary>
        public byte[] Data { get; set; }

        /// <summary>Идентификатор</summary>
        public Guid FileId { get; set; }
    }
}
