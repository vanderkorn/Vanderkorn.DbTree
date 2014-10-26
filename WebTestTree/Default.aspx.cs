using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Specialized;
using Tree;
using System.Collections.Generic;
using System.Text;

namespace WebTestTree
{
    public partial class _Default : System.Web.UI.Page
    {
        /// <summary>
        /// Текущая страница
        /// </summary>
        TreeNodeDataBase currentPage;

        /// <summary>
        /// Дерево страниц
        /// </summary>
        public static ITree<PageNode> tree;

        /// <summary>
        /// Обработчик события загрузки страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {   
               UrlPathHelper urlPath = new UrlPathHelper(this.UrlPath);
               currentPage = urlPath.GetPageNode();
               if (currentPage.Name == "-1")  return;
               this.Title = currentPage.Value.Title;
                switch (urlPath.Type)
                {
                    case WebTestTree.UrlPathHelper.TypePage.AddPage:
                            AddPanel.Visible = true;

                        break;
                    case WebTestTree.UrlPathHelper.TypePage.EditPage:
                            AddPanel.Visible = true;
                            nameLabel.Visible = false;
                            nameTextBox.Visible = false;
                            nameTextBox.Enabled = false;
                            CreateButton.Text = "Редактировать";
                            CreateButton.Click -= new EventHandler(CreateButton_Click);
                            CreateButton.Click += new EventHandler(EditButton_Click);
                            RequiredFieldValidator1.Enabled = false;
                            RegularExpressionValidator1.Enabled = false;
                            if (!this.IsPostBack)
                            {
                                titleTextBox.Text = currentPage.Value.Title;
                                descriptionTextBox.Text = currentPage.Value.Content;
                            }
                       
                        break;
                    case WebTestTree.UrlPathHelper.TypePage.RemovePage:
                        TreeNodeDataBase parent = (TreeNodeDataBase)currentPage.Parent;
                        string url = string.Empty;
                        if (parent != null)
                        {
                          currentPage.Remove();
                          url = string.Format("http://{0}:{1}{2}", Request.Url.Host, Request.Url.Port, parent.GetPath());
                        }
                        else
                            url = string.Format("http://{0}:{1}{2}", Request.Url.Host, Request.Url.Port, currentPage.GetPath());
                        Response.Redirect(url);
                        break;
                    default:
                            AddPanel.Visible = false;
                            HiddenFieldId.Value = currentPage.Name;
                        break;
                }
                RegularExpressionValidator1.ValidationExpression = "[a-zA-Z0-9]+";
                if (!this.IsPostBack)
                {
                    string host = string.Format("http://{0}:{1}", Request.Url.Host, Request.Url.Port);
                    AddLink.NavigateUrl = string.Format("{0}{1}", host, urlPath.GetUrl(WebTestTree.UrlPathHelper.TypePage.AddPage));
                    EditLink.NavigateUrl = string.Format("{0}{1}", host, urlPath.GetUrl(WebTestTree.UrlPathHelper.TypePage.EditPage));
                    RemoveLink.NavigateUrl = string.Format("{0}{1}", host, urlPath.GetUrl(WebTestTree.UrlPathHelper.TypePage.RemovePage));


                    var list = currentPage.PathToRoot();
                    tree = currentPage.GetSubTree(3);

                    FillSiteMapPath(list);
                    Label4.Text += "<br />";
                    FillTreeView((TreePageNode)tree.Root);
                }

        }

        /// <summary>
        /// Построить горизонтальную навигацию
        /// </summary>
        /// <param name="list">Коллекция узлов</param>
        private void FillSiteMapPath(IList<INode<PageNode>> list)
        {
            StringBuilder path = new StringBuilder();
            foreach (DBNode<PageNode> node in list)
            {
                TreeNodeDataBase pageNode = new TreeNodeDataBase() { Name = node.Name };
                path.AppendFormat("<a href='{0}'>{1}</a>->", pageNode.GetPath(), pageNode.Value.Title);
            }
            Label4.Text = path.ToString();
        }

        /// <summary>
        /// Построить дерево навигации
        /// </summary>
        /// <param name="root">Корневая страница</param>
        private void FillTreeView(TreePageNode root)
        {
            StringBuilder probel = new StringBuilder();
            for (int i = 0; i < ((int)root.Level+1); i++)
                probel.Append("&nbsp;");
            StringBuilder path = new StringBuilder();
            path.AppendFormat("{0}<a href='{1}'>{2}</a><br />", probel.ToString(), root.Value.Url, root.Value.Title);
            Label5.Text += path.ToString();
            foreach (TreePageNode node in root.ChildNodes)
                FillTreeView(node);
        }

        /// <summary>
        /// URL-страницы
        /// </summary>
        protected string UrlPath
        {
            get
            {
                if (HttpContext.Current.Items.Contains("UrlPath") && HttpContext.Current.Items["UrlPath"]!=null)
                    return HttpContext.Current.Items["UrlPath"].ToString();
                else return string.Empty;
            }
        }

        /// <summary>
        /// Обработчик события редактирования страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditButton_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                string title = titleTextBox.Text;
                string description = descriptionTextBox.Text;
                PageNode newPage = new PageNode()
                {
                    Title = title,
                    Content = description
                };
                currentPage.EditNode(newPage);

               string url = string.Format("http://{0}:{1}{2}", Request.Url.Host, Request.Url.Port, currentPage.GetPath());
              Response.Redirect(url);
       
            }
        }

        /// <summary>
        /// Обработчик события добавления страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CreateButton_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                string name = nameTextBox.Text;
                string title = titleTextBox.Text;
                string description = descriptionTextBox.Text;
                PageNode newPage = new PageNode()
                {
                    Name = name,
                    Title = title,
                    Content = description
                };
                TreeNodeDataBase page = (TreeNodeDataBase)currentPage.AddChildNode(newPage);
                if (page.Name == "-1") return;
               string url = string.Format("http://{0}:{1}{2}", Request.Url.Host, Request.Url.Port, page.GetPath());
               Response.Redirect(url);
            }
            }
    }
}