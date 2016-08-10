using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.ServiceFactory
{
    public interface IDynamicEndpointBehavior
    {
        string GetEndpointByServiceTypeName(string serviceTypeName);
    }
}
