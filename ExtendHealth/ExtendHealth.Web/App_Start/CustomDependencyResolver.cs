using ExtendHealth.Modules.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtendHealth.Web
{
    public class CustomDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private IInjectionContainer container;

        public CustomDependencyResolver(IInjectionContainer container)
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