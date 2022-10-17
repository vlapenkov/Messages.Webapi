using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Messages.Common.Helpers
{
    /// <summary>
    /// Узел для генерации дерева
    /// </summary>
    /// <typeparam name="T">тип узла</typeparam>
    public class TreeNode<T> where T : class
    {

        public TreeNode(T data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        #region PrivateMembers

        private  IList<TreeNode<T>> _children = new List<TreeNode<T>>();
        
        /// <summary> Данные узла </summary>
        public virtual T Data { get; private set; }

        /// <summary> Дочерние элементы </summary>
        public virtual IEnumerable<TreeNode<T>> Children => _children;

        /// <summary> Родительский элемент </summary>
        [JsonIgnore]
        public TreeNode<T> Parent { get; private set; }

        #endregion
        

        /// <summary> Добавить дочерний элемент </summary>
        public void AddChild(TreeNode<T> child)
        {
            if (child==null) throw new ArgumentNullException(nameof(child));
            
            child.Parent = this;
            
            _children.Add(child);
        }
    }
}
