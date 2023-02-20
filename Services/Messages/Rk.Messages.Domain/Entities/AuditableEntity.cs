using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Аудируемая сущность
    /// </summary>
    public class AuditableEntity :BaseEntity
    {
        protected AuditableEntity() { 
        }

        public string CreatedBy { get; protected set; } = null!;

        public string LastModifiedBy { get; protected set; } = null!;

        public DateTime Created { get; protected set; }

        public DateTime LastModified { get; protected set; }

        public void SetCreated(string userName) { 
            Created = DateTime.Now;
            LastModified = DateTime.Now;
            CreatedBy = userName;
            LastModifiedBy = userName;
        }

        public void SetLastModified(string userName)
        {           
            LastModified = DateTime.Now;         
            LastModifiedBy = userName;
        }
    }
}
