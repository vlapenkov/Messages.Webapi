using System;

namespace Rk.Messages.Logic.CommonNS.Dto
{
    public record AuditableEntityDto
    {
        public string CreatedBy { get;  set; }

        public string LastModifiedBy { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}
