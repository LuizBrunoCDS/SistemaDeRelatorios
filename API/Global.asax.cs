using API.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace API
{
#pragma warning disable ET001 // Type name does not match file name
    public class WebApiApplication : HttpApplication
#pragma warning restore ET001 // Type name does not match file name
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
