﻿using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;

namespace MyPortal
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Directory.CreateDirectory(@"C:\MyPortal\Files\Results");
        }
        
        #if !DEBUG
        protected void Application_BeginRequest()
        {
            if (Request.Url.Scheme != "http") return;
            var path = "https://" + Request.Url.Host + Request.Url.PathAndQuery;
            Response.Status = "301 Moved Permanently";
            Response.AddHeader("Location", path);
        }
        #endif        
    }
}