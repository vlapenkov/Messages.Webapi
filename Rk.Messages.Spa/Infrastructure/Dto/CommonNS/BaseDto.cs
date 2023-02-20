namespace Rk.Messages.Spa.Infrastructure.Dto.CommonNS
{
    /// <summary>
    /// Базовая dto
    /// </summary>
    public record BaseDto
    {
        /// <summary>Идентификатор</summary>
        public long Id { get; set; }

        /// <summary>наименование</summary>
        public string Name { get; set; }
    }
}
