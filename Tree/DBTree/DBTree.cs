using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Tree
{
    /// <summary>
    /// Дерево для работы с Базой Данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DBTree<T>  :ITree<T>
    {
        private string name;

        /// <summary>
        /// Родительский узел
        /// </summary>
        private DBNode<T> root;

        #region Члены ITree

        /// <summary>
        /// Корень дерева
        /// </summary>
        public INode<T> Root
        {
            get { return root; }
        }

        /// <summary>
        /// Имя дерева
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
        /// Создать корень
        /// </summary>
        /// <param name="root">Узел дерева</param>
        public void CreateRootNode(INode<T> root)
        {
            this.root = (DBNode<T>)root;
        }

        /// <summary>
        /// Создать корень дерева
        /// </summary>
        /// <param name="root">Значение</param>
        public void CreateRootNode(T root)
        {
           // this.root = (DBNode<T>)root;
        }

        #endregion
    }
}
