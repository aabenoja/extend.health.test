using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ExtendHealth.Modules.IoC
{
    public class ContainerResult : IContainerResult
    {
        private LifeCycle _lifeCycle;
        private Type _resultType;


        public LifeCycle LifeCycle { get { return _lifeCycle; } }
        public Type ResultType { get { return _resultType; } }
        public object Instance { get; set; }                        //should only be used for singleton objects

        public ContainerResult(Type resultType, LifeCycle lifeCycle)
        {
            _lifeCycle = lifeCycle;
            _resultType = resultType;
            Instance = null;
        }
    }
}
