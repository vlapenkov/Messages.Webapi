using MediatR;
using Rk.Messages.Logic.SectionsNS.Dto;

namespace Rk.Messages.Logic.SectionsNS.Queries.GetSectionsTree
{
    /// <summary>
    /// Запрос получения дерева разделов
    /// </summary>
    public class GetSectionTreeQuery : IRequest<SectionTreeNode>
    {
        /// <summary>Родительский раздел</summary>
        public long? ParentSectionId { get; set; }
    }
}
