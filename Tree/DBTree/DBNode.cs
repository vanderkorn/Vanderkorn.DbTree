using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Tree
{
    /// <summary>
    /// Узел дерева для работы с Базой Данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DBNode<T> : INode<T>
    {
        /// <summary>
        /// Провайдер Базы Данных
        /// </summary>
        public AbstractDataProvider<T> dataPovider;

        /// <summary>
        /// Имя узла (идентификатор)
        /// </summary>
        private string name;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DBNode()
        {
            dataPovider = AbstractDataProvider<T>.Instance;
        }

        #region Члены INode

        /// <summary>
        /// Родитель узла
        /// </summary>
        public INode<T> Parent
        {
            get
            {
                return dataPovider.GetParent(name);
            }
            set
            {
                dataPovider.SetParent(name, value.Name);
            }
        }

        /// <summary>
        /// Значение узла
        /// </summary>
        public T Value
        {
            get
            {
                return dataPovider.GetValueNode(name);
            }
            set
            {
                dataPovider.SetValueNode(name, value);
            }
        }
      
        /// <summary>
        /// Уровень узла
        /// </summary>
        public uint Level
        {
            get
            {
                return dataPovider.GetLevel(name);
            }
            set
            {
                dataPovider.SetLevel(name, value);
            }
        }

        /// <summary>
        /// Дочерные узлы 
        /// </summary>
        public IList<INode<T>> ChildNodes
        {
            get { return dataPovider.GetChildNodes(name); }
        }

        /// <summary>
        /// Имя узла (идентификатор)
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
        /// Добавить дочерный узел
        /// </summary>
        /// <param name="newNode">Узел</param>
        public void AddChildNode(INode<T> newNode)
        {
            dataPovider.AddChildNode(name, newNode);
        }

        /// <summary>
        /// Добавить дочерный узел
        /// </summary>
        /// <param name="newNode">Значение узла</param>
        /// <returns>Дочерний узел</returns>
        public INode<T> AddChildNode(T newNode)
        {
            return dataPovider.AddChildNode(name, newNode);
        }

        /// <summary>
        /// Получить поддерево
        /// </summary>
        /// <param name="depth">Высота поддерева</param>
        /// <returns>Дерево</returns>
        public virtual ITree<T> GetSubTree(uint depth)
        {
            Tree<T> newTree = new Tree<T>();
            newTree.CreateRootNode(new TreeNode<T>());
            newTree.Root.Name = this.name;
            newTree.Root.Level = this.Level;
            ((TreeNode<T>)(newTree.Root)).Value = this.Value;
            if (depth == 0)
                return newTree;
            depth--;
            foreach (DBNode<T> node in this.ChildNodes)
            {
                Tree<T> newTree2 = (Tree<T>)(node.GetSubTree(depth));
                newTree.Root.AddChildNode(newTree2.Root);
            }
            return newTree;
        }

        /// <summary>
        /// Полуучить поддерево
        /// </summary>
        /// <returns>Дерево</returns>
        public virtual ITree<T> GetSubTree()
        {
            Tree<T> newTree = new Tree<T>();
            newTree.CreateRootNode(new TreeNode<T>());
            newTree.Root.Name = this.name;
            newTree.Root.Level = this.Level;
            ((TreeNode<T>)(newTree.Root)).Value = this.Value;
            foreach (DBNode<T> node in this.ChildNodes)
            {
                Tree<T> newTree2 = (Tree<T>)(node.GetSubTree());
                newTree.Root.AddChildNode(newTree2.Root);
            }
            return newTree;
        }

        /// <summary>
        /// Изменить родительский узел
        /// </summary>
        /// <param name="newParent">Новый родительский узел</param>
        public void ChangeParentNode(INode<T> newParent)
        {
            this.Parent = newParent;
        }

        /// <summary>
        /// Удалить узел
        /// </summary>
        public void Remove()
        {
            dataPovider.RemoveNode(name);
        }

        /// <summary>
        /// Путь до корня дерева
        /// </summary>
        /// <returns>Коллекция узлов до корня</returns>
        public virtual IList<INode<T>> PathToRoot()
        {
            TreeNodeCollection<T> list = new TreeNodeCollection<T>();
            if (this.Parent != null)
            {
                TreeNodeCollection<T> list2 = new TreeNodeCollection<T>();
                list2 = (TreeNodeCollection<T>)this.Parent.PathToRoot();
                list.AddRange(list2);
            }
            list.Add(this);
            return list;
        }
        #endregion
    }
}
