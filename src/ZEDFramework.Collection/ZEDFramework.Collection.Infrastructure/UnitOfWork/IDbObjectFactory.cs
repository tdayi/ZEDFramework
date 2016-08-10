using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.UnitOfWork
{
    public interface IDbObjectFactory
    {
        DbObject CreateInstance();
        DbObject CreateInstance(bool entityLazyLoad);
        DbObject CreateInstance(bool entityLazyLoad, string cnString);
    }
}
