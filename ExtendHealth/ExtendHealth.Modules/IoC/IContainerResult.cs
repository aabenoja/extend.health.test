using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendHealth.Modules.IoC
{
    public interface IContainerResult
    {
        LifeCycle LifeCycle { get; }
        Type ResultType { get; }
        object Instance { get; set; }
    }
}
