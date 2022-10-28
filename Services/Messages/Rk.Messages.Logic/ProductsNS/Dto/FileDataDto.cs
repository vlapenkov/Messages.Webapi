using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Dto
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
