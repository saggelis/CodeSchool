using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FlipWeen.Common.Data;
using FlipWeen.Data;

namespace Flipween.Core.IoC
{
    public static class CastleHelper
    {
        public static WindsorContainer Container { get; private set; }
        private static WindsorHttpDependencyResolver _resolver;
        private static bool _initialized;

        static CastleHelper()
        {
            Container = new WindsorContainer();
            _initialized = false;
        }

        public static WindsorHttpDependencyResolver GetDependencyResolver()
        {
            if (_initialized)
                return _resolver;

            _initialized = true;
            //Container.Install(FromAssembly.This());
            RegisterTypes(Container);
            _resolver = new WindsorHttpDependencyResolver(Container);

            return _resolver;
        }
        
        //private static void BootstrapContainer()
        //{
        //    var container = new WindsorContainer().Install(FromAssembly.This());
        //    var controllerFactory = new WindsorControllerFactory(container.Kernel);
        //    ControllerBuilder.Current.SetControllerFactory(controllerFactory);

        //    RegisterTypes(container);
        //}

        private static void RegisterTypes(IWindsorContainer container)
        {
            //container
            //    .Register(Types
            //        .FromAssemblyInDirectory(new AssemblyFilter(AssemblyDirectory).FilterByName(x => x.FullName.Contains("FlipWeen")))
                   
            //        .Pick()
            //        .If(x => x.IsPublic)
            //        .If(x => x.GetInterfaces().Length > 0)
            //        .WithService
            //        .FirstInterface()
            //        .LifestylePerWebRequest());

            //container
            //    .Register(Classes
            //        .FromAssemblyInDirectory(new AssemblyFilter(AssemblyDirectory).FilterByName(x => x.FullName.Contains("FlipWeen")))
            //        .Pick()
            //        .If(x => x.IsPublic)
            //        .If(x => x.GetInterfaces().Length > 0)
            //        .WithService
            //        .FirstInterface()
            //        .LifestylePerWebRequest());


            container.Register(//Component.For<IDbContext>()
            //                  .ImplementedBy<DataContext>()
            //                  .LifestylePerWebRequest(),
                     Component.For(typeof(IDataRepository))
                              .ImplementedBy(typeof(DataRepository))
                              .LifestylePerWebRequest());

            container.Register(Classes.FromThisAssembly()
             .BasedOn<ApiController>()
             .LifestylePerWebRequest());
        }

        static public string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;

                var uri = new UriBuilder(codeBase);

                var path = Uri.UnescapeDataString(uri.Path);

                return Path.GetDirectoryName(path);
            }
        }
    }

}