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

namespace WebTestTree
{
    /// <summary>
    /// Класс для работы с текущим URL и страницей
    /// </summary>
    public class UrlPathHelper
    {
        /// <summary>
        /// URL
        /// </summary>
        private string urlPath;

        /// <summary>
        /// Тип страницы
        /// </summary>
        TypePage type;

        /// <summary>
        /// Части URL
        /// </summary>
        private string[] pathArray;

        /// <summary>
        /// Типы страниц
        /// </summary>
        public enum TypePage {ContentPage,AddPage,RemovePage,EditPage};

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="urlPath">URL</param>
        public UrlPathHelper(string urlPath)
        {
            this.urlPath=urlPath;
            type = GetTypePage();
        }

        /// <summary>
        /// Тип страницы
        /// </summary>
        public TypePage Type
        {
            get { return type; }
        }

        /// <summary>
        /// URL
        /// </summary>
        public string Path
        {
            get { return urlPath; }
        }

        /// <summary>
        /// Получить тип страницы
        /// </summary>
        /// <returns></returns>
        private  TypePage GetTypePage()
        {
            pathArray=urlPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (pathArray.Length != 0) 
            {
                string type=pathArray[pathArray.Length - 1];
                switch (type)
                {
                    case "add":
                        return TypePage.AddPage;
                        break;
                    case "remove":
                        return TypePage.RemovePage;
                        break;
                    case "edit":
                        return TypePage.EditPage;
                        break;
                }
            }
            return TypePage.ContentPage;
        }

        /// <summary>
        /// Получить страницу
        /// </summary>
        /// <returns>Страница</returns>
        public TreeNodeDataBase GetPageNode()
        { int level = 0;
            string name = string.Empty;
            if (type == TypePage.ContentPage)
            {
                if (pathArray.Length == 0)
                {
                    name = "/";
                    level = 0;
                }
                else
                {
                    name = pathArray[pathArray.Length - 1];
                    level = pathArray.Length;
                }
            }
            else
            {
                if (pathArray.Length < 2)
                {
                    name = "/";
                    level = 0;
                }
                else
                {
                    name = pathArray[pathArray.Length - 2];
                    level = pathArray.Length-1;
                }
            }
            var dataProvider = SqlDataProvider.Instance;
            dataProvider.ConnectionString = ConfigurationManager.ConnectionStrings["PagesConnectionString"].ConnectionString;


            int res = dataProvider.GetPageId(name, level);

            TreeNodeDataBase page = new TreeNodeDataBase();
            page.Name = res.ToString();
            page.Level = (uint)level;
            return page;
        }

        /// <summary>
        /// Получить новый URL
        /// </summary>
        /// <param name="_type">Тип новой страницы</param>
        /// <returns>Новый URL</returns>
        public string GetUrl(TypePage _type)
        {
            string url=string.Empty;
            switch (_type)
                {
                    case TypePage.AddPage:
                        url = string.Format("{0}/{1}", GetPageNode().GetPath(), "add");
                        break;
                    case TypePage.EditPage:
                        url = string.Format("{0}/{1}", GetPageNode().GetPath(), "edit");
                        break;
                    case TypePage.RemovePage:
                        url = string.Format("{0}/{1}", GetPageNode().GetPath(), "remove");
                        break;
                    case TypePage.ContentPage:
                        url = GetPageNode().GetPath();
                        break;
                }

            url = url.Replace("//","/");
            return url;
        }
    }
}
