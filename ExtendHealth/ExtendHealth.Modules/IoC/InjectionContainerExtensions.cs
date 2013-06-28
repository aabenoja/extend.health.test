using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendHealth.Modules.IoC
{
    public static class InjectionContainerExtensions
    {
        /// <summary>
        /// Allows for a more readable way to invoke the IInjectionContainer.Resolve method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public static T Resolve<T>(this IInjectionContainer container)
        {
            return (T) container.Resolve(typeof(T));
        }

        /// <summary>
        /// Clears the InjectionContainer's internal storage, removing all registered bindings. (Should only be used for testing purposes)
        /// </summary>
        /// <param name="container"></param>
        public static void ClearContainer(this InjectionContainer container)
        {
            container.containerDict.Clear();
        }
    }
}
