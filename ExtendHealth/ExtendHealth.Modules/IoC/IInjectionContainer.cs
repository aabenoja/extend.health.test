using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendHealth.Modules.IoC
{
    public interface IInjectionContainer
    {
        void Register<TAbstract, TConcrete>(LifeCycle cycleType = LifeCycle.Transient);
        T Resolve<T>();
    }
}
