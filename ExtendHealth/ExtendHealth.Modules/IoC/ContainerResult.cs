using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ExtendHealth.Modules.IoC
{
    public interface IContainerResult
    {
        LifeCycle LifeCycle { get; }
        Type ResultType { get; }
    }

    public struct ContainerResult<T> : IContainerResult
    {
        private LifeCycle _lifeCycle;

        public LifeCycle LifeCycle { get { return _lifeCycle; } }
        public Type ResultType { get { return typeof(T); } }

        public ContainerResult(LifeCycle lifeCycle)
        {
            _lifeCycle = lifeCycle;
        }
    }
}
