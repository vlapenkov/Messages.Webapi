namespace Rk.Messages.Spa.Infrastructure.Dto.CommonNS
{
    /// <summary>
    /// Информация о файлах для продукции
    /// </summary>
    public record FileDataDto
    {
        public string FileName { get; set; }

        public byte[] Data { get; set; }

        public Guid FileId { get; set; }
    }
}
