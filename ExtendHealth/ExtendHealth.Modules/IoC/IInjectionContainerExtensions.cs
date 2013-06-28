using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendHealth.Modules.IoC
{
    public class IInjectionContainerExtensions
    {
        public static T Resolve<T>(this IInjectionContainer container)
        {
            return (T) container.Resolve(typeof(T));
        }
    }
}
