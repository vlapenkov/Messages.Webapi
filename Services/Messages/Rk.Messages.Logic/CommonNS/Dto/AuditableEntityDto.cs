using System;

namespace Rk.Messages.Logic.CommonNS.Dto
{
    public record AuditableEntityDto
    {
        public virtual string CreatedBy { get;  set; }

        public virtual string LastModifiedBy { get; set; }

        public virtual DateTime Created { get; set; }

        public virtual DateTime LastModified { get; set; }
    }
}
