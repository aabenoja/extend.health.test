using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendHealth.Modules.IoC
{
    public class UnregisteredTypeException : Exception
    {
        public UnregisteredTypeException() : base("The requested type has not been registered in the container.") { }
    }
}
