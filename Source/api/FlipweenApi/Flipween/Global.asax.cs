using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Flipween.Core.Mappings;

namespace FlipWeen
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //private WindsorContainer _windsorContainer;
        protected void Application_Start()
        {
            //InitializeWindsor();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();
        }

        protected void Application_End()
        {
            //if (_windsorContainer != null)
            //{
            //    _windsorContainer.Dispose();
            //}
        }

        //private void InitializeWindsor()
        //{
        //    _windsorContainer = new WindsorContainer();
        //    _windsorContainer.Install(FromAssembly.This());
        //    var dir = AppDomain.CurrentDomain.BaseDirectory;
        //    _windsorContainer.Register(
        //        Types.FromAssemblyInDirectory(new AssemblyFilter(dir, "FlipWeen*"))
        //        );

        //}
    }
}
