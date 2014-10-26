using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
namespace Tree
{
    /// <summary>
    /// Коллекция узлов дерева
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class TreeNodeCollection<T> : Collection<INode<T>>
    {
        /// <summary>
        /// Добавить коллекцию
        /// </summary>
        /// <param name="collection">Коллекция</param>
        public void AddRange(TreeNodeCollection<T> collection)
        {
            foreach (var item in collection)
                this.Add(item);
        }
    }
}
