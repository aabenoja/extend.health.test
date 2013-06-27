using ExtendHealth.Modules.IoC;
using ExtendHealth.Web.Repositories;
using ExtendHealth.Web.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ExtendHealth.Web
{
    public static class DependencyInjectorConfig
    {
        public static void RegisterTypes(HttpConfiguration config)
        {
            IInjectionContainer container = new InjectionContainer();

            container.Register<IUsersRepo, MockUsersRepo>(LifeCycle.Singleton);
            container.Register<IContactsRepo, MockContactsRepo>();

            
        }
    }
}