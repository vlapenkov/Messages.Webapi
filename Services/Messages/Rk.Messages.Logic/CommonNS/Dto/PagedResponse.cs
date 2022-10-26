using System.Collections.Generic;

namespace Rk.Messages.Logic.CommonNS.Dto
{
    /// <summary>
    /// Пагинированный ответ для списка типа T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record PagedResponse<T>
    {
        public int FirstItemOnPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public int LastItemOnPage { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public IEnumerable<T> Rows { get; set; }
    }
}
