using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ExtendHealth.Modules.IoC
{
    public struct ContainerResult : IContainerResult
    {
        private LifeCycle _lifeCycle;
        private Type _resultType;


        public LifeCycle LifeCycle { get { return _lifeCycle; } }
        public Type ResultType { get { return _resultType; } }
        //public ResultType Instance
        //{
        //    get
        //    {
        //        return null;
        //    }
        //}

        public ContainerResult(Type resultType, LifeCycle lifeCycle)
        {
            _lifeCycle = lifeCycle;
            _resultType = resultType;
        }
    }

    public interface IContainerResult
    {
        LifeCycle LifeCycle { get; }
        Type ResultType { get; }
    }
}
