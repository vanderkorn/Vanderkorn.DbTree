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
    /// Узел дерева
    /// </summary>
    public class TreePageNode : TreeNode<PageNode>
    {
        /// <summary>
        /// Получить URL-узла
        /// </summary>
        /// <returns>URL-узла</returns>
        public string GetPath()
        {
            var list = PathToRoot();

            StringBuilder path = new StringBuilder();
            if (list.Count == 1)
            {
                var node = list[0];
                if (node.Value.Name == "/")
                    return node.Value.Name;
            }
            foreach (TreePageNode node in list)
            {
                if (node.Value.Name == "/") continue;
                path.AppendFormat("/{0}", node.Value.Name);
            }
            return path.ToString();
        }
    }
}
