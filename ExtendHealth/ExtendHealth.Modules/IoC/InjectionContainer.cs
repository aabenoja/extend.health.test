using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendHealth.Modules.IoC
{
    public class InjectionContainer : IInjectionContainer
    {
        private Dictionary<Type, Type> containerDict;

        public InjectionContainer()
        {
            containerDict = new Dictionary<Type, Type>();
        }

        public void Register<TAbstract, TConcrete>(LifeCycle cycleType = LifeCycle.Transient)
        {
            containerDict.Add(typeof(TAbstract), typeof(TConcrete));
        }

        public TAbstract Resolve<TAbstract>()
        {
            Type requestedType = containerDict[typeof(TAbstract)];
            throw new NotImplementedException();
        }
    }
}
