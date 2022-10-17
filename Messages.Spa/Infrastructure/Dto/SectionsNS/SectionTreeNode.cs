using Messages.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Spa.Infrastructure.Dto.SectionsNS
{
    /// <summary>
    /// Дерево разделов
    /// </summary>
    public class SectionTreeNode : TreeNode<SectionDto>
    {
        public SectionTreeNode(SectionDto data) : base(data)
        { }
    }
}
