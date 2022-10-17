using Messages.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Logic.SectionsNS.Dto
{
    /// <summary>
    /// Узел для раздела
    /// </summary>
    public class SectionTreeNode : TreeNode<SectionDto>
    {
        public SectionTreeNode(SectionDto data) : base(data)
        {}
    }
}
