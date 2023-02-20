using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    /// <summary>
    /// Сортировка продукции по атрибутам
    /// </summary>
    public enum OrderByProduct
    {
        NameByAsc = 0,
        NameByDesc,
        RegionByAsc,
        RegionByDesc,
        ProducerByAsc,
        ProducerByDesc,
        RatingByAsc,
        RatingByDesc,
        IdByAsc,
        IdByDesc
    }
}
