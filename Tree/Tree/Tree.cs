    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections.ObjectModel;
    namespace Tree
    {
        /// <summary>
        /// Дерево
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Tree<T> : ITree<T>
        {
            /// <summary>
            /// Имя дерева
            /// </summary>
            private string name;

            /// <summary>
            /// Родительский узел
            /// </summary>
            private TreeNode<T> root;

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
            /// Создать корневой узел
            /// </summary>
            /// <param name="root"></param>
            public void CreateRootNode(INode<T> root)
            {
                this.root = (TreeNode<T>)root;
            }

            /// <summary>
            /// Создать корневой узел
            /// </summary>
            /// <param name="root"></param>
            public void CreateRootNode(T root)
            {
                TreeNode<T> newNode = new TreeNode<T>();
                newNode.Parent = null;
                newNode.Value = root;
                newNode.Level = 0;
                this.root = newNode;
            }
            #endregion
        }
      
    }

   


