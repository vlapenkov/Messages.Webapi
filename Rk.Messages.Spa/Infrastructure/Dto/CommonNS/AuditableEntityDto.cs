namespace Rk.Messages.Spa.Infrastructure.Dto.CommonNS
{
    public record AuditableEntityDto
    {
        public long Id { get; set; }

        public string CreatedBy { get;  set; }

        public string LastModifiedBy { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}
