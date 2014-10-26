using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Tree;
using System.Text;

namespace WebTestTree
{
    /// <summary>
    /// Узел дерева для работы с БД
    /// </summary>
    public class TreeNodeDataBase : DBNode<PageNode>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public TreeNodeDataBase()
        {
            dataPovider = SqlDataProvider.Instance;
            dataPovider.ConnectionString = ConfigurationManager.ConnectionStrings["PagesConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Получить поддерво
        /// </summary>
        /// <returns>Дерево</returns>
        public override ITree<PageNode> GetSubTree()
        {
            TreePage newTree = new TreePage();
            newTree.CreateRootNode(new TreePageNode());
            newTree.Root.Name = this.Name;
            newTree.Root.Level = this.Level;
            newTree.Root.Value.Url = this.GetPath();
            ((TreePageNode)(newTree.Root)).Value = this.Value;
            foreach (var node in this.ChildNodes)
            {
                TreePage newTree2 = (TreePage)(node.GetSubTree());
                newTree.Root.AddChildNode(newTree2.Root);
            }
            return newTree;
        }

        /// <summary>
        /// Получить поддерево
        /// </summary>
        /// <param name="depth">Высота поддерва</param>
        /// <returns>Дерево</returns>
        public override ITree<PageNode> GetSubTree(uint depth)
        {
            TreePage newTree = new TreePage();
            newTree.CreateRootNode(new TreePageNode());
            newTree.Root.Name = this.Name;
            newTree.Root.Level = this.Level;

            ((TreePageNode)(newTree.Root)).Value = this.Value;
            newTree.Root.Value.Url = this.GetPath();
            if (depth == 0)
                return newTree;
            depth--;
            foreach (var node in this.ChildNodes)
            {
                TreePage newTree2 = (TreePage)(node.GetSubTree(depth));
                newTree.Root.AddChildNode(newTree2.Root);
            }
            return newTree;
        }

        /// <summary>
        /// Получить URL-узла
        /// </summary>
        /// <returns>URL-узла</returns>
        public string GetPath()
        {
            var list = this.PathToRoot();
            StringBuilder path = new StringBuilder();
            if (list.Count == 1)
            {
                var node =list[0];
                if (node.Value.Name == "/")
                    return node.Value.Name;
            }
            foreach (TreeNodeDataBase node in list)
            {
                if (node.Value.Name == "/") continue;
                path.AppendFormat("/{0}", node.Value.Name);
            }
            return path.ToString();
        }

        /// <summary>
        /// Редактировать узел
        /// </summary>
        /// <param name="newPage">Новое значени узла</param>
        internal void EditNode(PageNode newPage)
        {
            ((SqlDataProvider)dataPovider).EditNode(Name, newPage);
        }
    }
}
