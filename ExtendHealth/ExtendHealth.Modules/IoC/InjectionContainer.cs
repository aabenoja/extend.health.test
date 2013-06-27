using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendHealth.Modules.IoC
{
    public class InjectionContainer : IInjectionContainer
    {
        private Dictionary<Type, IContainerResult> containerDict;

        public InjectionContainer()
        {
            containerDict = new Dictionary<Type, IContainerResult>();
        }

        public object Resolve(Type type)
        {
            try
            {
                var containerResult = containerDict[type];
                object output = containerResult.Instance;

                if (output == null)
                {
                    var constructor = containerResult.ResultType.GetConstructors().FirstOrDefault();
                    var paramTypes = constructor.GetParameters().Select(x => x.ParameterType);
                    var resolveInfo = this.GetType().GetMethod("Resolve");
                    var resolvedParams = paramTypes.Select(x => resolveInfo.Invoke(this, new object[]{x})).ToArray();
                    output = constructor.Invoke(resolvedParams);

                    if (containerResult.LifeCycle == LifeCycle.Singleton)
                        containerResult.Instance = output;
                }

                return output;
            }
            catch (KeyNotFoundException)
            {
                throw new UnregisteredTypeException();
            }
        }

        public void Register<TAbstract, TConcrete>(LifeCycle cycleType = LifeCycle.Transient) where TConcrete : TAbstract
        {
            containerDict.Add(typeof(TAbstract), new ContainerResult(typeof(TConcrete), cycleType));
        }
    }
}
