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
using System.Web.Routing;

namespace ExtendHealth.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //DependencyInjectorConfig.RegisterTypes(GlobalConfiguration.Configuration);
            DependencyResolver.SetResolver(new CustomDependencyResolver(CreateContainer()));
        }

        private IInjectionContainer CreateContainer()
        {
            IInjectionContainer container = new InjectionContainer();

            container.Register<IUsersRepo, MockUsersRepo>(LifeCycle.Singleton);
            container.Register<IContactsRepo, MockContactsRepo>();

            container.Register<HomeController, HomeController>();

            return container;
        }
    }

}