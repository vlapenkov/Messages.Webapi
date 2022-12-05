using Rk.Messages.Logic.CommonNS.Dto;
using System;

namespace Rk.Messages.Logic.NewsNS.Dto
{
    public record NewsResponse : AuditableEntityDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? DocumentId { get; set; }
    }
}
