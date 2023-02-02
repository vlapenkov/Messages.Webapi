using Rk.Messages.Domain.Entities.Products;
using System;
using System.ComponentModel.DataAnnotations;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Отзыв о товаре
    /// </summary>
    public class Review : AuditableEntity
    {
        private static readonly byte _maxRating = 5;
       
        public Review(long baseProductId, string description, byte rating)
        {
            if (rating > _maxRating || rating <= 0) throw new Exception("Рейтинг товара должен быть от 1 до 5");

            Description = description;
            BaseProductId = baseProductId;
            Rating = rating;
        }

        public virtual BaseProduct BaseProduct { get; }
        public long BaseProductId { get; protected set; }

        [StringLength(4096)]
        public string Description { get; protected set; }

        public byte Rating { get; protected set; }
    }
}
