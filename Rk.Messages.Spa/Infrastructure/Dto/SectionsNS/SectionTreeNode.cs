using Rk.Messages.Common.Helpers;

namespace Rk.Messages.Spa.Infrastructure.Dto.SectionsNS
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
