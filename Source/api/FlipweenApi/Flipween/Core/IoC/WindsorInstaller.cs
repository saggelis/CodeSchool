using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FlipWeen.Common.Data;
using FlipWeen.Data;

namespace Flipween.Core.IoC
{
    public class WebApiControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<ApiController>()
                .LifestylePerWebRequest());
        }
    }

    public class PersistenceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Component.For<DbContext>()
            //                            .ImplementedBy<DataContext>()
            //                            .LifestylePerWebRequest()
            //                  );

            container.Register(Component.For<IDbContext>()
                              .ImplementedBy<DataContext>()
                              .LifestylePerWebRequest(),
                     Component.For(typeof(IDataRepository))
                              .ImplementedBy(typeof(BaseDataRepository))
                              .LifestylePerWebRequest());
        }
        
    }

    //public class ServiceInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component
    //            .For<ITestService>()
    //            .ImplementedBy<TestService>()
    //            .LifestylePerWebRequest());
    //    }
    //}
}