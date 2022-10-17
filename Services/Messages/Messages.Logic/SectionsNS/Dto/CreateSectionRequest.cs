﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Logic.SectionsNS.Dto
{
    /// <summary>
    /// Запрос создания раздела
    /// </summary>
    public class CreateSectionRequest
    {
        /// <summary>Родительский раздел</summary>
        public long? ParentSectionId { get; set; }

        /// <summary>Наименование</summary>
        public string Name { get; set; }
    }
}
