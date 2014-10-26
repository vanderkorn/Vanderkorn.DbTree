using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using System.Web.Routing;
using System.Web.Compilation;
using System.Web.UI;
namespace WebTestTree
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes); 
        }
        void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(
               "View Page",
               new Route("{*PageName}", new PageRouteHandler("Default.aspx"))
            );

        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
    public class PageRouteHandler : IRouteHandler
    {
        private string _page;
        public PageRouteHandler(string page)
        {
            _page = page;
        }
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            string productName = requestContext.RouteData.Values["PageName"] as string;
            HttpContext.Current.Items["UrlPath"] = productName;
            return BuildManager.CreateInstanceFromVirtualPath("~/" + _page, typeof(Page)) as Page;
        }
    } 

}