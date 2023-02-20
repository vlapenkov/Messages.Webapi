namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Файл организации
    /// </summary>
    public class OrganizationDocument : BaseEntity
    {

        public OrganizationDocument(Document document)
        {
            Document = document;
        }

        public OrganizationDocument(long organizationId, long documentId)
        {
            OrganizationId = organizationId;

            DocumentId = documentId;
        }

        public long OrganizationId { get; private set; }

        public virtual Organization Organization { get; } = null!;

        public long DocumentId { get; private set; }

        public virtual Document Document { get; } = null!;

    }
}
