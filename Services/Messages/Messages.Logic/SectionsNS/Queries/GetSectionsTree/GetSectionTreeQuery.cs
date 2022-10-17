using MediatR;
using Messages.Logic.SectionsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Logic.SectionsNS.Queries.GetSectionsTree
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
