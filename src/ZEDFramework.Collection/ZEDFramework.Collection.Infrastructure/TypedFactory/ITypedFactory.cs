using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.TypedFactory
{
    public interface ITypedFactory
    {
        T Resolve<T>();
        IEnumerable<T> ResolveAll<T>();
    }
}
