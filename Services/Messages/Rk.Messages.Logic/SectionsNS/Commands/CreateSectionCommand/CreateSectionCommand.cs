using MediatR;

namespace Rk.Messages.Logic.SectionsNS.Commands.CreateSectionCommand
{
    /// <summary>
    /// Команда для создания раздела
    /// </summary>
    public class CreateSectionCommand : IRequest<long>
    {
        /// <summary>Id Родительского раздела</summary>
        public long? ParentSectionId { get; set; }

        /// <summary>Наименование раздела</summary>
        public string Name { get; set; }
    }
}
