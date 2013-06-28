using ExtendHealth.Modules.IoC;
using ExtendHealth.Web.Controllers;
using ExtendHealth.Web.Repositories;
using ExtendHealth.Web.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ExtendHealth.Web
{
    public static class DependencyInjectorConfig
    {
        public static void RegisterTypes()
        {
            IInjectionContainer container = new InjectionContainer();

            container.Register<IUsersRepo, MockUsersRepo>(LifeCycle.Singleton);
            container.Register<IContactsRepo, MockContactsRepo>();
            container.Register<HomeController, HomeController>();

            DependencyResolver.SetResolver(new CustomDependencyResolver(container));
        }
    }
}