using ExtendHealth.Modules.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtendHealth.Web
{
    public class MyDependencyResolver : IDependencyResolver
    {
        private IInjectionContainer container;

        public MyDependencyResolver(IInjectionContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (UnregisteredTypeException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return new List<object> { container.Resolve(serviceType) };
            }
            catch (UnregisteredTypeException)
            {
                return new List<Object>();
            }
        }
    }
}