using System;
using System.Collections.Generic;
using System.Text;
namespace Tree
{
    /// Интерфейс узла дерева
    /// </summary>
    public interface INode<T>
    {
        /// <summary>
        /// Родитель узла
        /// </summary>
        INode<T> Parent { get; set; }

        /// <summary>
        /// Уровень дерева
        /// </summary>
        uint Level { get; set; }

        /// <summary>
        /// Значение узла
        /// </summary>
        T Value { get; set; }

        /// <summary>
        /// Имя узла
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Получить список дочерних узлов 1-го уровня
        /// </summary>
        IList<INode<T>> ChildNodes { get; }

        /// <summary>
        /// Добавить дочерний узел
        /// </summary>
        void AddChildNode(INode<T> newNode);

        /// <summary>
        /// Добавить дочерний узел
        /// </summary>
        INode<T> AddChildNode(T newNode);

        /// <summary>
        /// Получить поддерево данного узла с заданной глубиной
        /// </summary>
        /// <param name="depth">Глубина поддерева</param>
        /// <returns>Поддерево</returns>
        ITree<T> GetSubTree(uint depth);

        /// <summary>
        /// Получить поддерево данного узла
        /// </summary>
        /// <returns>Поддерево</returns>
        ITree<T> GetSubTree();

        /// <summary>
        /// Изменить родительский узел
        /// </summary>
        /// <param name="newParent">Новый родитель</param>
        void ChangeParentNode(INode<T> newParent);

        /// <summary>
        /// Удалить узел
        /// </summary>
        void Remove();

        /// <summary>
        /// Получить путь до корня
        /// </summary>
        /// <returns></returns>
        IList<INode<T>> PathToRoot();
    }
}
