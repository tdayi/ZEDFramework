using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin(bool useWithoutTransaction = false);
        void Commit();
    }
}
