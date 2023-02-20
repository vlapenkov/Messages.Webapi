using Rk.Messages.Domain.Entities;
using Rk.Messages.Logic.CommonNS.Dto;
using System;

namespace Rk.Messages.Logic.SectionsNS.Dto
{
    public record SectionDto :AuditableEntityDto
    {
        public long? ParentSectionId { get; set; }       
        public string Name { get; set; }
        public Guid? DocumentId { get; set; }

    }
}
