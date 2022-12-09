using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Новости
    /// </summary>
    public class News :AuditableEntity
    {
        public News( string name, string description, Guid? documentId)
        {            
            Name = name;
            Description = description;
            DocumentId = documentId;
        }


        [StringLength(1024)]
        public string Name { get; protected set; }

        [StringLength(4096)]
        public string Description { get; protected set; }

        public Guid? DocumentId { get; protected set; }

    }
}
