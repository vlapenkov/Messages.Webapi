using Rk.Messages.Common.Helpers;

namespace Rk.Messages.Logic.SectionsNS.Dto
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
