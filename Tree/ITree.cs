using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace Tree
{
    /// <summary>
    /// Интерфейс дерева
    /// </summary>
    public interface ITree<T>
    {
        /// <summary>
        /// Корень дерева
        /// </summary>
        INode<T> Root { get; }

        /// <summary>
        /// Имя дерева
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Создать корень дерева
        /// </summary>
        /// <param name="root">Узел, который выступит в качестве корневого</param>
        void CreateRootNode(INode<T> root);

        /// <summary>
        /// Создать корень дерева
        /// </summary>
        /// <param name="root">Узел, который выступит в качестве корневого</param>
        void CreateRootNode(T root);
    }
}
