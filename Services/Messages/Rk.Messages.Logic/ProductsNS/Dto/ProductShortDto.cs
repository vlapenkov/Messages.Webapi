﻿using Rk.Messages.Logic.CommonNS.Dto;
using System;

namespace Rk.Messages.Logic.ProductsNS.Dto
{
    /// <summary>
    /// Информация о товаре для списка
    /// </summary>
    public record ProductShortDto :AuditableEntityDto
    {
        /// <summary>идентификатор продукта</summary>
        public long Id { get; set; }

        /// <summary>наименование продукции</summary>
        public string Name { get; set; }

        /// <summary>описание продукции</summary>
        public string Description { get; set; }

        /// <summary>цена</summary>
        public decimal Price { get; set; }

        /// <summary>Ссылка на документ</summary>
        public Guid? DocumentId { get; set; }

    }
}
