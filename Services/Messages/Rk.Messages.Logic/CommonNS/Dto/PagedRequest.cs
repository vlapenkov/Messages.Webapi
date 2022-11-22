namespace Rk.Messages.Logic.CommonNS.Dto
{
    /// <summary>
    /// Базовый запрос для пагинации
    /// </summary>
    public record PagedRequest
    {
        const int _defaultPageSize = 100;
        
        /// <summary>номер страницы</summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>размер страницы</summary>
        public int PageSize { get; set; } = _defaultPageSize;

        public static int DefaultPageSize = _defaultPageSize;
    }
}
