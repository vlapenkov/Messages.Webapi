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
        /// <summary>Имя файла</summary>
        public string? FileName { get; set; }

        /// <summary>Данные</summary>
        public byte[] Data { get; set; }

        /// <summary>Идентификатор</summary>
        public Guid FileId { get; set; }
    }
}
