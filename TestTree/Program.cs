using System;
using System.Collections.Generic;
using System.Text;
using Tree;

namespace TestTree
{
    public interface ITest
    {
        void good();
    }
    public class test: ITest
    {
        public void good()
        {
            return;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //ITree<string> stringTree = new NewTree<string>()
            //{
            //    Name = "tree"
            //};
            //stringTree.CreateRootNode("11");

            //for (int i = 0; i < 4; i++)
            //{
            //    NewTreeNode<string> node = new NewTreeNode<string>() { Name = "1" + i.ToString() };

            //    stringTree.Root.AddChildNode(node);

            //    for (int j = 0; j < 4; j++)
            //    {
            //        NewTreeNode<string> node2 = new NewTreeNode<string>() { Name = "2" + j.ToString() };
            //        node.AddChildNode(node2);
            //    }
            //}
            //printTree(stringTree);

         //   printTree(stringTree.Root.GetSubTree(1));
            //TreeNode<string> stringRoot = new TreeNode<string>() { 
            //    Name="root"
            //};
            //Tree<string> stringTree = new Tree<string>()
            //    {
            //        Name="tree"
            //    };

            //stringTree.CreateRootNode(stringRoot);


            //for (int i = 0; i < 4;i++ )
            //{
            //    TreeNode<string> node = new TreeNode<string>(){Name="1"+i.ToString()};

            //    stringTree.Root.AddChildNode(node);

            //    for (int j= 0; j< 4; j++)
            //    {
            //        TreeNode<string> node2 = new TreeNode<string>() { Name = "2" + j.ToString() };
            //        node.AddChildNode(node2);
            //    }
            //}

            //printTree(stringTree);

            //printTree(stringTree.Root.GetSubTree(1));
            Console.ReadLine();


        }
        static void printTree<T>(ITree<T> tree)
        {
            printNode(tree.Root);
        }
        static void printNode<T>(INode<T> node)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < node.Level; i++)
            {
                str.Append(" ");
            }
            Console.WriteLine(str.ToString()+node.Name);

            foreach (INode<T> node2 in node.ChildNodes)
            {
                printNode<T>(node2);
            }
        }

    }
}
