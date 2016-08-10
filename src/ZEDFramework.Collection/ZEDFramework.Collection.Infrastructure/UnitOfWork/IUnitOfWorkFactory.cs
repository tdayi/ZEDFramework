using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
        IUnitOfWork Create(UnitOfWorkBehavior behavior);
        IUnitOfWork GetCurrent();
        IUnitOfWork GetCurrent(string factoryKey);
        object GetCurrentDbObject { get; }
    }
}
