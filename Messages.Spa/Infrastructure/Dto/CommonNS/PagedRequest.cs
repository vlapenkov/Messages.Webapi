using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Spa.Infrastructure.Dto.CommonNS
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
    }
}
