namespace Rk.Messages.Logic.SectionsNS.Dto
{
    /// <summary>
    /// Запрос создания раздела
    /// </summary>
    public class CreateSectionRequest
    {
        /// <summary>Родительский раздел</summary>
        public long? ParentSectionId { get; set; }

        /// <summary>Наименование</summary>
        public string Name { get; set; }
    }
}
