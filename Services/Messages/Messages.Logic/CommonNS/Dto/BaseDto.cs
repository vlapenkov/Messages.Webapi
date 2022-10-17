using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Logic.CommonNS.Dto
{
    /// <summary>
    /// Базовая dto
    /// </summary>
    public class BaseDto
    {
        /// <summary>Идентификатор</summary>
        public long Id { get; set; }

        /// <summary>наименование</summary>
        public string Name { get; set; }
    }
}
