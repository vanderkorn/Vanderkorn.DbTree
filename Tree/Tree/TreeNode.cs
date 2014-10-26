using System;
using System.Collections.Generic;
using System.Text;
namespace Tree
{
    /// <summary>
    /// Узел дерева
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNode<T> : INode<T>
    {
        /// <summary>
        /// Имя узла
        /// </summary>
        private string name;

        /// <summary>
        /// Значение узла
        /// </summary>
        private T value;

        /// <summary>
        /// Уровень узла
        /// </summary>
        private uint level = 0;

        /// <summary>
        /// Родительский узел
        /// </summary>
        private TreeNode<T> parent;

        /// <summary>
        /// Дочерние узлы 1-го уровня
        /// </summary>
        private TreeNodeCollection<T> childNodes = new TreeNodeCollection<T>();

        /// <summary>
        /// Значение узла
        /// </summary>
        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        #region Члены INode

        /// <summary>
        /// Родитель узла
        /// </summary>
        public INode<T> Parent
        {
            get { return parent; }
            set { parent = (TreeNode<T>)value; }
        }

        /// <summary>
        /// Дочерние узлы
        /// </summary>
        public IList<INode<T>> ChildNodes
        {
            get { return childNodes; }
        }

        /// <summary>
        /// Имя узла
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Уровень узла
        /// </summary>
        public uint Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }

        /// <summary>
        /// Добавить дочерний узел
        /// </summary>
        /// <param name="newNode0"></param>
        public void AddChildNode(INode<T> newNode0)
        {
            TreeNode<T> newNode = (TreeNode<T>)newNode0;
            newNode.Parent = this;
            childNodes.Add(newNode);
            newNode.Level = (this.Level + 1);
        }

        /// <summary>
        /// Добавить дочерний узел
        /// </summary>
        /// <param name="newNode0"></param>
        public INode<T> AddChildNode(T newNode0)
        {
            TreeNode<T> newNode = new TreeNode<T>();
            newNode.value = newNode0;
            newNode.Parent = this;
            childNodes.Add(newNode);
            newNode.Level = (this.Level + 1);
            return newNode;
        }

        /// <summary>
        /// Получить поддерево, заданной глубины
        /// </summary>
        /// <param name="depth">Высота дерева</param>
        /// <returns>Дерево</returns>
        public ITree<T> GetSubTree(uint depth)
        {
            Tree<T> Tree = new Tree<T>();
            Tree.CreateRootNode(new TreeNode<T>());
            Tree.Root.Name = this.Name;
            Tree.Root.Level = this.Level;
            ((TreeNode<T>)(Tree.Root)).Value = this.Value;
            ((TreeNode<T>)(Tree.Root)).childNodes.Clear();
            if (depth == 0)
                return Tree;
            depth--;
            foreach (TreeNode<T> node in this.childNodes)
            {
                Tree<T> Tree2 = (Tree<T>)(node.GetSubTree(depth));
                Tree.Root.AddChildNode(Tree2.Root);
            }
            return Tree;
        }

        /// <summary>
        /// Получить поддерево
        /// </summary>
        /// <returns>Дерево</returns>
        public ITree<T> GetSubTree()
        {
            Tree<T> Tree = new Tree<T>();
            Tree.CreateRootNode(this);
            return Tree;
        }

        /// <summary>
        /// Изменить родительский узел
        /// </summary>
        /// <param name="newParent"></param>
        public void ChangeParentNode(INode<T> newParent)
        {
            this.parent.childNodes.Remove(this);//убрать всех детей
            newParent.AddChildNode(this);//изменить родителя
        }

        /// <summary>
        /// Получить путь до корня
        /// </summary>
        /// <returns></returns>
        public IList<INode<T>> PathToRoot()
        {
            TreeNodeCollection<T> path = new TreeNodeCollection<T>();
            if (parent != null)
            {
                TreeNodeCollection<T> pathParent = (TreeNodeCollection<T>)parent.PathToRoot();
                path.AddRange(pathParent);
            } 
            path.Add(this);
            return path;
        }

        /// <summary>
        /// Удалить узел
        /// </summary>
        public void Remove()
        {
            if (this.parent != null)
            {
                ((TreeNodeCollection<T>)(this.parent.ChildNodes)).Remove(this);//удалим ссылку с родителя
                foreach (TreeNode<T> node in this.childNodes)//перенесем детей
                {
                    ((TreeNodeCollection<T>)(this.parent.ChildNodes)).Add(node);
                }
                this.parent = null;//уберем ссылку
                this.childNodes.Clear();//очистим детей
            }
        }
        #endregion
    }
}
