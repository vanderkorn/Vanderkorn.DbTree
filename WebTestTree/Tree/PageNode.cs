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

namespace WebTestTree
{
    /// <summary>
    /// Класс страницы
    /// </summary>
    public class PageNode
    {
        /// <summary>
        /// Название страницы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Заголовок страницы
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Содержание страницы
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// URL-страницы
        /// </summary>
        public string Url { get; set; }
    }
}
